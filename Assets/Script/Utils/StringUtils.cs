//***********************************************************
// 描述：string 扩展类
// 作者：fanwei 
// 创建时间：2021-03-13 18:14:31 
// 版本：1.0 
// 备注：
//***********************************************************
/// <summary>
/// string 扩展类
/// </summary>
public static class StringUtils
{
    /// <summary>
    /// 将一个string 转化为 int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        return int.Parse(str);
    }

    /// <summary>
    /// 将一个string 转化为 float
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        return float.Parse(str);
    }

    /// <summary>
    /// 将一个string 转化为 long
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long ToLong(this string str)
    {
        return long.Parse(str);
    }
}
