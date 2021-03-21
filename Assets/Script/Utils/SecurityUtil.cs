//***********************************************************
// 描述：加密工具
// 作者：fanwei 
// 创建时间：2021-03-21 18:12:28 
// 版本：1.0 
// 备注：
//***********************************************************

/// <summary>
/// 加密工具
/// </summary>
public sealed class SecurityUtil 
{

    //异或因子
    private static byte[] xorScale = new byte[] { 45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171 };//.data文件的xor加解密因子



    private SecurityUtil()
    {

    }


    /// <summary>
    /// 对字节数组进行异或
    /// </summary>
    /// <param name="bufffer"></param>
    /// <returns></returns>
    public static byte[] Xor(byte [] buffer)
    {
        int iScaleLen = xorScale.Length;
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (byte)(buffer[i] ^ xorScale[i % iScaleLen]);
        }

        return buffer;
    }

}
