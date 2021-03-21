//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-18 17:56:47 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetWorkSocket : SingletonMono<NetWorkSocket>
{
    /// <summary>
    /// 客户端socket
    /// </summary>
    private Socket m_ClientSocket;

    #region 发送数据包 属性
    /// <summary>
    /// 消息队列
    /// </summary>
    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();
    /// <summary>
    /// 检查消息队列
    /// </summary>
    private Action m_CheckSendQueue;

    /// <summary>
    /// 压缩最小字节书
    /// </summary>
    private const int m_CompressLen = 200;
    #endregion

    #region 接受数据包 属性
    /// <summary>
    /// 接受数据包的字节数组缓冲区
    /// </summary>
    private byte[] m_ReceiveBuffer = new byte[10240];
    /// <summary>
    /// 接受数据包的缓冲数据流
    /// </summary>
    private MMO_MemoryStream m_ReceiveMs = new MMO_MemoryStream();
    /// <summary>
    /// 收到的消息队列
    /// </summary>
    private Queue<Byte[]> m_ReceiveQueue = new Queue<byte[]>();

    /// <summary>
    /// 每帧处理包数量
    /// </summary>
    private int m_ReceiveIndex;
    #endregion


    #region Connect 连接服务器
    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <param name="ip">ip</param>
    /// <param name="prot">端口号</param>
    public void Connect(string ip, int prot)
    {
        if (m_ClientSocket != null && m_ClientSocket.Connected) return;

        //创建socket
        m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //链接服务器
        m_ClientSocket.Connect(new IPEndPoint(IPAddress.Parse(ip), prot));

        Debug.Log("连接服务器成功");

        m_CheckSendQueue = CheckSendQueue;

        //开始接受服务器消息
        ReceiveMsg();

        
    }
    #endregion

    //======================================

    #region CheckSendQueue 检查消息队列
    /// <summary>
    /// 检查消息队列
    /// </summary>
    private void CheckSendQueue()
    {
        lock (m_SendQueue)
        {
            if (m_SendQueue.Count > 0)
            {
                Send(m_SendQueue.Dequeue());
            }
        }
    }
    #endregion

    #region SendMsg 发送消息到服务器
    /// <summary>
    ///  发送消息到服务器
    /// </summary>
    /// <param name="buffer"></param>
    public void SendMsg(byte[] buffer)
    {
        //封包
        byte[] data = MakeData(buffer);

        //加入队列
        lock (m_SendQueue)
        {
            m_SendQueue.Enqueue(data);

            //检查队列
            m_CheckSendQueue.BeginInvoke(null, null);
        }
    }
    #endregion

    #region MakeData 封包
    /// <summary>
    /// 封包
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    private byte[] MakeData(byte[] buffer)
    {
        byte[] data;

        //1.是否压缩
        bool isCompress = false;
        if (buffer.Length > m_CompressLen)
        {
            isCompress = true;
            buffer = ZlibHelper.CompressBytes(buffer);
        }

        //2.异或
        buffer = SecurityUtil.Xor(buffer);

        //3.循环校验
        ushort cyc = Crc16.CalculateCrc16(buffer);


        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort((ushort)(buffer.Length + 3));
            ms.WriteBool(isCompress);
            ms.WriteUShort(cyc);
            ms.Write(buffer, 0, buffer.Length);

            data = ms.ToArray();
        }
        return data;
    }
    #endregion

    #region Send 发送消息到服务器
    /// <summary>
    /// 发送数据到服务器 
    /// </summary>
    /// <param name="buffer">数据包</param>
    private void Send(byte[] buffer)
    {
        m_ClientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBack, m_ClientSocket);
    }
    #endregion

    #region SendCallBack 发送消息回调
    /// <summary>
    /// 发送消息回调
    /// </summary>
    /// <param name="ar"></param>
    private void SendCallBack(IAsyncResult ar)
    {
        m_ClientSocket.EndSend(ar);

        //检查队列
        m_CheckSendQueue.BeginInvoke(null, null);
    }
    #endregion

    //======================================
    #region ReceiveMsg 接受服务器消息
    /// <summary>
    /// 接受消息
    /// </summary>
    private void ReceiveMsg()
    {

        m_ClientSocket.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_ClientSocket);
    }
    #endregion

    #region ReceiveCallBack  接受服务器消息回调
    /// <summary>
    /// 接受服务器消息回调
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {


        try
        {
            int len = m_ClientSocket.EndReceive(ar);

            if (len > 0)
            {
                //重置缓冲流位置
                m_ReceiveMs.Position = m_ReceiveMs.Length;
                //写入数据
                m_ReceiveMs.Write(m_ReceiveBuffer, 0, len);

                //如果缓存数据流的长度>2 说明至少有个不完整的包过来了
                //为什么这里是2 因为我们客户端封装数据包 用的ushort 长度就是2
                if (m_ReceiveMs.Length > 2)
                {
                    //进行循环 拆分数据包
                    while (true)
                    {
                        //把数据流指针位置放在0处
                        m_ReceiveMs.Position = 0;
                        //currMsgLen = 包体的长度
                        int currMsgLen = m_ReceiveMs.ReaduUShort();
                        //currFullMsgLen 总包的长度=包头长度+包体长度
                        int currFullMsgLen = 2 + currMsgLen;

                        //如果数据流的长度>=整包的长度 说明至少收到了一个完整包
                        if (m_ReceiveMs.Length >= currFullMsgLen)
                        {
                            //定义包体的byte[]数组
                            byte[] buffer = new byte[currMsgLen];
                            //把数据流指针放到2的位置 也就是包体的位置
                            m_ReceiveMs.Position = 2;
                            //实际内容
                            m_ReceiveMs.Read(buffer, 0, currMsgLen);

                            //加入队列中
                            lock (m_ReceiveQueue)
                            {
                                m_ReceiveQueue.Enqueue(buffer);
                            }

                            //=======================剩余字节数组============================
                            //剩余字节长度
                            int remainLen = (int)m_ReceiveMs.Length - currFullMsgLen;

                            if (remainLen > 0)
                            {
                                //把指针放在第一个包的尾部
                                m_ReceiveMs.Position = currFullMsgLen;
                                //定义剩余字节数组
                                byte[] remainBuffer = new byte[remainLen];
                                //把数据流读到剩余字节数组
                                m_ReceiveMs.Read(remainBuffer, 0, remainLen);

                                //清空数据流
                                m_ReceiveMs.Position = 0;
                                m_ReceiveMs.SetLength(0);

                                //把剩余字节数组重新写入数据流
                                m_ReceiveMs.Write(remainBuffer, 0, remainLen);

                                remainBuffer = null;
                            }
                            else
                            {
                                //没有剩余字节

                                //清空数据流
                                m_ReceiveMs.Position = 0;
                                m_ReceiveMs.SetLength(0);
                                break;
                            }

                        }
                        else
                        {
                            //还没有收到完整包
                            break;
                        }
                    }
                }
                //进行下一次接收数据包
                ReceiveMsg();
            }
            else
            {
                //断开连接
                Debug.LogFormat("服务器{0}断开连接", m_ClientSocket.RemoteEndPoint.ToString());
            }
        }
        catch (Exception ex)
        {
            //断开连接
            Debug.LogFormat("服务器{0}断开连接,原因{1}", m_ClientSocket.RemoteEndPoint.ToString(), ex.Message);

        }
    }
    #endregion

    /// <summary>
    /// 处理收到的数据包
    /// </summary>
    protected override void OnUpdate()
    {
        base.OnUpdate();

        m_ReceiveIndex = 0;

        while (true)
        {
            if (m_ReceiveIndex < 5)
            {
                m_ReceiveIndex++;
                lock (m_ReceiveQueue)
                {
                    if (m_ReceiveQueue.Count > 0)
                    {
                        byte[] buffer = m_ReceiveQueue.Dequeue();

                        //处理读到的包体内容
                        //1byte 压缩标志 2byte crc循环校验码 异或之后数组
                        bool isCompress = false;
                        ushort crc = 0;
                        byte[] bufferNew = new byte[buffer.Length - 3];

                        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                        {
                            isCompress = ms.ReadBool();
                            crc = ms.ReaduUShort();
                            ms.Read(bufferNew, 0, bufferNew.Length);
                        }

                        //计算Crc
                        ushort crcNew = Crc16.CalculateCrc16(bufferNew);
                        if (crc == crcNew)
                        {
                            //1.异或
                            bufferNew = SecurityUtil.Xor(bufferNew);


                            //2.解压缩
                            if (isCompress)
                            {
                                bufferNew = ZlibHelper.DeCompressBytes(bufferNew);
                            }


                            //3.实际数据包内容
                            ushort protoCode = 0;
                            byte[] msgData = new byte[bufferNew.Length - 2];
                            using (MMO_MemoryStream ms = new MMO_MemoryStream(bufferNew))
                            {
                                //包体前两位为 协议号
                                protoCode = ms.ReaduUShort();
                                ms.Read(msgData, 0, msgData.Length);
                            }

                            EventDispatch.Instance.Dispatch(protoCode, msgData);
                        }
                       
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }
    }


    /// <summary>
    /// 销毁
    /// </summary>
    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        if(m_ClientSocket !=null && m_ClientSocket.Connected)
        {
            m_ClientSocket.Shutdown(SocketShutdown.Both);
            m_ClientSocket.Close();
        }
    }
}
