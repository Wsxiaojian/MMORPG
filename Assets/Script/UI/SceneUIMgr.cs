//***********************************************************
// 描述：这是一个功能代码
// 作者：fanwei 
// 创建时间：2021-02-20 17:06:03
// 版 本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIMgr : Singleton<SceneUIMgr>
{
    /// <summary>
    /// 窗口UI类型
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

    public GameObject LoadSceneUI(SceneUIType sceneUIType)
    {
        return null;
    }
}
