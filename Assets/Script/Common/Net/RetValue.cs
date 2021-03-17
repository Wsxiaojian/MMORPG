//***********************************************************
// 描述：网络返回消息
// 作者：fanwei 
// 创建时间：2021-03-17 18:01:36 
// 版本：1.0 
// 备注：
//***********************************************************
/// <summary>
/// 网络返回消息
/// </summary>
public class RetValue
{
    /// <summary>
    /// 是否有错误
    /// </summary>
    public bool HasError;
    /// <summary>
    /// 错误信息
    /// </summary>
    public string ErrorMsg;
    /// <summary>
    /// 结果数据
    /// </summary>
    public string RetData;
}
