//***********************************************************
// 描述：所有枚举
// 作者：fanwei 
// 创建时间：2021-02-20 17:22:06
// 版 本：1.0
// 备注：
//***********************************************************

#region 角色动画相关
/// <summary>
/// 角色类型
/// </summary>
public enum RoleType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 主角
    /// </summary>
    MainPlayer,
    /// <summary>
    /// 怪
    /// </summary>
    Monster,
}

/// <summary>
/// 角色状态
/// </summary>
public enum RoleStateType
{
    None =0,
    Idle = 1,
    Run = 2,
    Attack = 3,
    Hurt = 4,
    Die = 5
}
/// <summary>
/// 角色动画名称
/// </summary>
public enum RoleAnimName
{
    Idle_Normal,
    Idle_Fight,
    PhyAttack1,
    PhyAttack2,
    PhyAttack3,
    Run,
    Hurt,
    Die
}
/// <summary>
/// 转换参数名称
/// </summary>
public enum TransToName
{
    ToIdle,
    ToRun,
    ToHurt,
    ToDie,
    ToPhyAttack,
    CurState,
}
#endregion

#region 场景类型 SceneType
/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
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


/// <summary>
/// 提示信息显示类型
/// </summary>
public enum MessageViewType
{
    /// <summary>
    /// 只显示ok
    /// </summary>
    MVT_OK,
    /// <summary>
    /// 显示Ok 和 Cancel
    /// </summary>
    MVT_OkAndCancel,
}

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
    /// <summary>
    /// 角色信息窗口
    /// </summary>
    RoleInfo,

    /// <summary>
    /// 游戏服进入
    /// </summary>
    GameServerEnter,
    /// <summary>
    /// 游戏区服选择
    /// </summary>
    GameServerSelect,
}
#endregion

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

#region 窗口显示样式 WindowShowStyle
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

