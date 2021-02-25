//***********************************************************
// 描述：UI窗口基类
// 作者：fanwei 
// 创建时间：2021-02-20 17:00:06
// 版 本：1.0
// 备注：
//***********************************************************
using UnityEngine;

/// <summary>
///   UI窗口基类
/// </summary>
public class UIWindowBase : UIBase
{
    /// <summary>
    /// 窗口挂载点
    /// </summary>
    public WindowUIContainerType WindowUIContainerType;

    /// <summary>
    /// 窗口动画类型
    /// </summary>
    public WindowShowStyle WindowShowStyle;

    /// <summary>
    /// 动画持续时间
    /// </summary>
    public float Duration;

    /// <summary>
    /// 当前UI 的类型
    /// </summary>
    [HideInInspector]
    public WindowUIType CurrentUIType;

    /// <summary>
    /// 下一个打开的窗口
    /// </summary>
    [HideInInspector]
    public WindowUIType NextWindowUIType;


    /// <summary>
    ///  关闭窗口
    /// </summary>
    protected virtual void Close()
    {
        WindowUIMgr.Instance.CloseWindow(CurrentUIType);
    }

    /// <summary>
    /// 销毁前  跳转下一个窗口
    /// </summary>
    protected override void BeforeDestroy()
    {
        LayerUIMgr.Instance.CheckOpenWindow();
        if (NextWindowUIType == WindowUIType.None) return;
        WindowUIMgr.Instance.OpenWindow(NextWindowUIType);
    }
}
