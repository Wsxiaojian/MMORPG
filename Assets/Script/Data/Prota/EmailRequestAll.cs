//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-19 19:07:50 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 获取Ema 信息
/// </summary>
public struct EmailRequestAll : IProto
{
    /// <summary>
    /// 协议编号
    /// </summary>
    public ushort ProtoCode { get { return 1001; } }

    public int EmailID;

    public string EmailInfo;

    /// <summary>
    /// 转化为字节数组
    /// </summary>
    /// <returns></returns>
    public  byte []  ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(EmailID);
            ms.WriteUTF8String(EmailInfo);

            return ms.ToArray();
        }
    }


    /// <summary>
    /// 获取一个
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static EmailRequestAll Get(byte[]  buffer)
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            EmailRequestAll email = new EmailRequestAll();
            email.EmailID = ms.ReadInt();
            email.EmailInfo = ms.ReadUTF8String();
            return email;
        }
    }
}
