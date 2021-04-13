//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-13 20:14:35 
// 版本：1.0 
// 备注：
//***********************************************************
using System;

/// <summary>
/// 加密 md5
/// </summary>
public static class EncryptUtil
{
    #region Md5
    /// <summary>
    /// Md5加密
    /// </summary>
    /// <param name="value">value</param>
    /// <returns></returns>
    public static string Md5(this string value)
    {
        if (string.IsNullOrEmpty( value))
        {
            return null;
        }
        System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bytResult = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(value));
        string strResult = BitConverter.ToString(bytResult);
        strResult = strResult.Replace("-", "");
        return strResult;
    }

    /// <summary>
    /// Md5加密
    /// </summary>
    /// <param name="value">value</param>
    /// <param name="Is16">是否16位密码</param>
    /// <returns></returns>
    public static string Md5(this string value, bool Is16)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }
        if (Is16)
        {

            return value.Md5().Substring(8, 16);
        }
        else
        {
            return value.Md5();
        }
    }
    #endregion Md5
}
