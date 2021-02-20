//***********************************************************
// 描述：这是一个功能代码
// 作者：fanwei 
// 创建时间：2021-02-20 17:00:06
// 版 本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public WindowUIMgr.WindowUIType CurrentUIType;

    /// <summary>
    /// 下一个打开的窗口
    /// </summary>
    public WindowUIMgr.WindowUIType NextWindowUIType;
}
