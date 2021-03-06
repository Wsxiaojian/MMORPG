//***********************************************************
// 描述：场景UI加载管理
// 作者：fanwei 
// 创建时间：2021-02-20 17:06:03
// 版 本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景UI加载
/// </summary>
public class UISceneCtrl : Singleton<UISceneCtrl>
{

    /// <summary>
    /// 当前UIscene
    /// </summary>
    public UISceneViewBase CurUIScene;
    

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
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene, "UI_Root_City");
                break;
        }

        if (obj == null) return null;

        CurUIScene = obj.GetComponent<UISceneViewBase>();

        return obj;
    }
}
