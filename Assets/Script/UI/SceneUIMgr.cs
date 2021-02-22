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
    /// 当前UIscene
    /// </summary>
    public UISceneBase CurUIScene;


    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="sceneUIType"></param>
    /// <returns></returns>
    public GameObject LoadSceneUI(SceneUIType sceneUIType)
    {
        GameObject obj = null;
        switch (sceneUIType)
        {
            case SceneUIType.LogOn:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene,"UI_Root_LogOn");
                break;
            case SceneUIType.Loading:
                break;
            case SceneUIType.MainCity:
                break;
        }

        if (obj == null) return null;

        CurUIScene = obj.GetComponent<UISceneBase>();

        return obj;
    }
}