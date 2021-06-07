//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-14 19:07:17 
// 版本：1.0 
// 备注：
//***********************************************************

/// <summary>
/// 区服信息
/// </summary>
public class RetGameServerEntity 
{
    /// <summary>
    /// 区服Id
    /// </summary>
    public int Id;

    /// <summary>
    /// 区服名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 服务器状态
    /// </summary>
    public int ServerStatus;

    /// <summary>
    /// 是否推荐
    /// </summary>
    public bool IsCommand;

    /// <summary>
    /// 是否新服
    /// </summary>
    public bool IsNew;

    /// <summary>
    /// 区服ip
    /// </summary>
    public string Ip;

    /// <summary>
    /// 服务器端口号
    /// </summary>
    public int Port;
}
