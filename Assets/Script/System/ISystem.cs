//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-26 09:17:36 
// 版本：1.0 
// 备注：
//***********************************************************

/// <summary>
/// 系统控制接口
/// </summary>
public interface ISystem 
{
    /// <summary>
    /// 打开某个界面
    /// </summary>
    /// <param name="type"></param>
    void  OpenView(WindowUIType type);
}
