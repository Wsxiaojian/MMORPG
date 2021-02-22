//***********************************************************
// 描述：所有枚举
// 作者：fanwei 
// 创建时间：2021-02-20 17:22:06
// 版 本：1.0
// 备注：
//***********************************************************

#region 窗口挂载点类型 WindowUIContainerType
/// <summary>
/// 窗口挂载点类型
/// </summary>
public enum WindowUIContainerType
{
    /// <summary>
    /// 居中
    /// </summary>
    Center,
    /// <summary>
    /// 左上
    /// </summary>
    TopLeft,
    /// <summary>
    /// 右上
    /// </summary>
    TopRight,
    /// <summary>
    /// 左下
    /// </summary>
    BottomLeft,
    /// <summary>
    /// 右下
    /// </summary>
    BottomRight,
}
#endregion

#region 窗口挂载点类型 WindowUIContainerType
/// <summary>
/// 窗口显示样式
/// </summary>
public enum WindowShowStyle
{
    /// <summary>
    /// 正常 无动画
    /// </summary>
    Normal,
    /// <summary>
    /// 中间放大
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上边出来
    /// </summary>
    FromTop,
    /// <summary>
    /// 从下边出来
    /// </summary>
    FromBottom,
    /// <summary>
    /// 从左边出来
    /// </summary>
    FromLeft,
    /// <summary>
    /// 从右边出来
    /// </summary>
    FromRight,
}
#endregion

#region 场景UI类型 SceneUIType
/// <summary>
/// 场景UI类型
/// </summary>
public enum SceneUIType
{
    /// <summary>
    /// 登陆注册
    /// </summary>
    LogOn,
    /// <summary>
    /// 加载场景
    /// </summary>
    Loading,
    /// <summary>
    /// 主城
    /// </summary>
    MainCity,
}
#endregion

#region 窗口UI类型 WindowUIType
/// <summary>
/// 窗口UI类型
/// </summary>
public enum WindowUIType
{
    /// <summary>
    /// 空
    /// </summary>
    None,
    /// <summary>
    /// 登陆
    /// </summary>
    LogOn,
    /// <summary>
    /// 注册
    /// </summary>
    Reg,
}
#endregion