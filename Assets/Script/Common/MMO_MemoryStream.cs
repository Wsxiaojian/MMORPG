//***********************************************************
// 描述：将通用类型转化为 byte数组 short ushort int uint long  ulong  float double string
// 作者：fanwei 
// 创建时间：2021-03-11 17:24:49 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.IO;
using System.Text;

/// <summary>
/// 转换byte数组  short ushort int uint long  ulong  float double string
/// </summary>
public class MMO_MemoryStream : MemoryStream
{
    #region short
    /// <summary>
    /// 从流中读取一个 short 字段
    /// </summary>
    /// <returns></returns>
    public short ReadShort()
    {
        byte[] buffer = new byte[2];
        base.Read(buffer, 0, 2);
        return BitConverter.ToInt16(buffer, 0);
    }
    /// <summary>
    /// 往流中写一个 short 字段
    /// </summary>
    /// <param name="data"></param>
    public void WriteShort(short data)
    {
        byte[] buffer = BitConverter.GetBytes(data);
        base.Write(buffer, 0, buffer.Length);
    }
    #endregion

    #region ushort
    /// <summary>
    /// 从流中读取一个 ushort 字段
    /// </summary>
    /// <returns></returns>
    public ushort ReaduUShort()
    {
        byte[] buffer = new byte[2];
        base.Read(buffer, 0, 2);
        return BitConverter.ToUInt16(buffer, 0);
    }
    /// <summary>
    /// 往流中写一个 ushort 字段
    /// </summary>
    /// <param name="data"></param>
    public void WriteUShort(ushort data)
    {
        byte[] buffer = BitConverter.GetBytes(data);
        base.Write(buffer, 0, buffer.Length);
    }
    #endregion

    #region int
    /// <summary>
    /// 从流中读取一个 int 字段
    /// </summary>
    /// <returns></returns>
    public int ReadInt()
    {
        byte[] buffer = new byte[4];
        base.Read(buffer, 0, 4);
        return BitConverter.ToInt32(buffer, 0);
    }
    /// <summary>
    /// 往流中写一个 int 字段
    /// </summary>
    /// <param name="data"></param>
    public void WriteInt(int data)
    {
        byte[] buffer = BitConverter.GetBytes(data);
        base.Write(buffer, 0, buffer.Length);
    }
    #endregion

    #region uint
    /// <summary>
    /// 从流中读取一个 uint 字段
    /// </summary>
    /// <returns></returns>
    public uint ReaduUInt()
    {
        byte[] buffer = new byte[4];
        base.Read(buffer, 0, 4);
        return BitConverter.ToUInt32(buffer, 0);
    }
    /// <summary>
    /// 往流中写一个 uint 字段
    /// </summary>
    /// <param name="data"></param>
    public void WriteUInt(uint data)
    {
        byte[] buffer = BitConverter.GetBytes(data);
        base.Write(buffer, 0, buffer.Length);
    }
    #endregion

    #region long
    /// <summary>
    /// 从流中读取一个 long 字段
    /// </summary>
    /// <returns></returns>
    public long ReadLong()
    {
        byte[] buffer = new byte[8];
        base.Read(buffer, 0, 8);
        return BitConverter.ToInt64(buffer, 0);
    }
    /// <summary>
    /// 往流中写一个 long 字段
    /// </summary>
    /// <param name="data"></param>
    public void WriteLong(long data)
    {
        byte[] buffer = BitConverter.GetBytes(data);
        base.Write(buffer, 0, buffer.Length);
    }
    #endregion

    #region ulong
    /// <summary>
    /// 从流中读取一个 uint 字段
    /// </summary>
    /// <returns></returns>
    public ulong ReaduULong()
    {
        byte[] buffer = new byte[8];
        base.Read(buffer, 0, 8);
        return BitConverter.ToUInt64(buffer, 0);
    }
    /// <summary>
    /// 往流中写一个 ulong 字段
    /// </summary>
    /// <param name="data"></param>
    public void WriteULong(ulong data)
    {
        byte[] buffer = BitConverter.GetBytes(data);
        base.Write(buffer, 0, buffer.Length);
    }
    #endregion

    #region float
    /// <summary>
    /// 从流中读取一个 float 字段
    /// </summary>
    /// <returns></returns>
    public float ReadFloat()
    {
        byte[] buffer = new byte[4];
        base.Read(buffer, 0, 4);
        return BitConverter.ToSingle(buffer, 0);
    }
    /// <summary>
    /// 往流中写一个 float 字段
    /// </summary>
    /// <param name="data"></param>
    public void WriteFloat(float data)
    {
        byte[] buffer = BitConverter.GetBytes(data);
        base.Write(buffer, 0, buffer.Length);
    }
    #endregion

    #region double
    /// <summary>
    /// 从流中读取一个 double 字段
    /// </summary>
    /// <returns></returns>
    public double ReadDouble()
    {
        byte[] buffer = new byte[8];
        base.Read(buffer, 0, 8);
        return BitConverter.ToDouble(buffer, 0);
    }
    /// <summary>
    /// 往流中写一个 long 字段
    /// </summary>
    /// <param name="data"></param>
    public void WriteDouble(double data)
    {
        byte[] buffer = BitConverter.GetBytes(data);
        base.Write(buffer, 0, buffer.Length);
    }
    #endregion

    #region string
    /// <summary>
    /// 从流中读取一个 string 字段
    /// </summary>
    /// <returns></returns>
    public string ReadString()
    {
        ushort count = ReaduUShort();
        byte[] buffer = new byte[count];
        base.Read(buffer, 0, count);
        return Encoding.UTF8.GetString(buffer);
    }
    /// <summary>
    /// 往流中写一个 string 字段
    /// </summary>
    /// <param name="data"></param>
    public void WriteString(string data)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(data);
        byte[] len = BitConverter.GetBytes((ushort)buffer.Length);
        base.Write(len, 0, 2);
        base.Write(buffer, 0, buffer.Length);
    }
    #endregion
}
