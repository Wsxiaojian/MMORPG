//***********************************************************
// 描述：这是一个功能代码
// 作者：fanwei 
// 创建时间：2021-02-20 17:05:52
// 版 本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗口UI管理器
/// </summary>
public class WindowUIMgr : Singleton<WindowUIMgr>
{
    /// <summary>
    /// 窗口UI类型
    /// </summary>
    public enum WindowUIType
    {
        /// <summary>
        /// 登陆
        /// </summary>
        LogOn,
        /// <summary>
        /// 注册
        /// </summary>
        Reg,
    }

    public GameObject OpenWindow(WindowUIType windowUIType)
    {
        return null;
    }

}
