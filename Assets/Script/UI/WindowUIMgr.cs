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
using DG.Tweening;

/// <summary>
/// 窗口UI管理器
/// </summary>
public class WindowUIMgr : Singleton<WindowUIMgr>
{


    /// <summary>
    /// 当前打开的窗口
    /// </summary>
    private Dictionary<WindowUIType, UIWindowBase> m_DicWindows = new Dictionary<WindowUIType, UIWindowBase>();


    /// <summary>
    /// 打开一个窗口
    /// </summary>
    /// <param name="windowUIType"></param>
    /// <returns></returns>
    public GameObject OpenWindow(WindowUIType windowUIType)
    {
        //当前已经打开了
        if (windowUIType == WindowUIType.None || m_DicWindows.ContainsKey(windowUIType)) return null;


        GameObject obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindow, string.Format("pan{0}", windowUIType), cache: true);
        if (obj == null) return null;

        UIWindowBase uIWindow = obj.GetComponent<UIWindowBase>();
        if (uIWindow == null) return null;

        m_DicWindows.Add(windowUIType, uIWindow);
        //当前ui类型
        uIWindow.CurrentUIType = windowUIType;


        switch (uIWindow.WindowUIContainerType)
        {
            case WindowUIContainerType.Center:
                obj.transform.parent = SceneUIMgr.Instance.CurUIScene.Center_Container;
                break;
            case WindowUIContainerType.TopLeft:

                break;
            case WindowUIContainerType.TopRight:

                break;
            case WindowUIContainerType.BottomLeft:

                break;
            case WindowUIContainerType.BottomRight:

                break;
        }

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localScale = Vector3.one;

        obj.SetActive(true);

        //打开动画
        StartOpenAnim(uIWindow, true);

        return obj;
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    /// <param name="windowUIType"></param>
    public void CloseWindow(WindowUIType windowUIType)
    {
        if (m_DicWindows.ContainsKey(windowUIType) == false) return;

        //关闭
        StartOpenAnim(m_DicWindows[windowUIType], false);
    }


    /// <summary>
    /// 删除窗口
    /// </summary>
    /// <param name="uIWindow"></param>
    private void DestoryWindowUI(UIWindowBase uIWindow)
    {
        if (m_DicWindows.ContainsKey(uIWindow.CurrentUIType))
        {
            m_DicWindows.Remove(uIWindow.CurrentUIType);
        }

        //移除
        Object.DestroyImmediate(uIWindow.gameObject);
    }


    /// <summary>
    /// 打开UI窗口动画
    /// </summary>
    /// <param name="uIWindow"></param>
    /// <param name="isOpen"></param>
    private void StartOpenAnim(UIWindowBase uIWindow , bool isOpen)
    {
        switch (uIWindow.WindowShowStyle)
        {
            case WindowShowStyle.Normal:
                NormalAnim(uIWindow, isOpen);
                break;
            case WindowShowStyle.CenterToBig:
                CenterToBigAnim(uIWindow, isOpen);
                break;
            case WindowShowStyle.FromTop:
                break;
            case WindowShowStyle.FromBottom:
                break;
            case WindowShowStyle.FromLeft:
                break;
            case WindowShowStyle.FromRight:
                break;

        }
    }

    /// <summary>
    /// 正常 无动画
    /// </summary>
    /// <param name="uIWindow"></param>
    /// <param name="isOpen"></param>
    private void NormalAnim(UIWindowBase uIWindow, bool isOpen)
    {
        if (isOpen)
        {
            uIWindow.gameObject.SetActive(true);
        }
        else
        {
            DestoryWindowUI(uIWindow);
        }
    }

    /// <summary>
    /// 中间放大动画
    /// </summary>
    /// <param name="uIWindow"></param>
    /// <param name="isOpen"></param>
    private void CenterToBigAnim(UIWindowBase uIWindow, bool isOpen)
    {
        if (isOpen)
        {
            uIWindow.gameObject.SetActive(true);
            uIWindow.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            uIWindow.transform.DOScale(Vector3.one, uIWindow.Duration);
        }
        else
        {
            uIWindow.transform.DOScale(Vector3.zero, uIWindow.Duration).OnComplete(
                () =>
                {
                    DestoryWindowUI(uIWindow);
                });
        }
    }
}
