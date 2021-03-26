//***********************************************************
// 描述：窗口UI管理器
// 作者：fanwei 
// 创建时间：2021-02-20 17:05:52
// 版 本：1.0
// 备注：
//***********************************************************
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 窗口UI管理器
/// </summary>
public class UIViewUtil : Singleton<UIViewUtil>
{

    /// <summary>
    /// 当前打开的窗口
    /// </summary>
    private Dictionary<WindowUIType, UIWindowBase> m_DicWindows = new Dictionary<WindowUIType, UIWindowBase>();
    
    /// <summary>
    /// 当前打开窗口数量
    /// </summary>
    public int OpenWindowNum
    {
        get
        {
            return m_DicWindows.Count;
        }
    }

    #region 打开/关闭一个窗口 
    /// <summary>
    /// 打开一个窗口
    /// </summary>
    /// <param name="windowUIType"></param>
    /// <returns></returns>
    public GameObject OpenWindow(WindowUIType windowUIType)
    {
        //当前已经打开了
        if (windowUIType == WindowUIType.None) return null;


        GameObject obj = null;
        UIWindowBase uIWindow;
        if ( m_DicWindows.ContainsKey(windowUIType) ==false)
        {
            //窗口命名规则需要统一 “ pan_ + 窗口类型枚举名称 ”
            obj =  ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindow, string.Format("pan_{0}", windowUIType), cache: true);

            if (obj == null) return null;

            uIWindow = obj.GetComponent<UIWindowBase>();

            if (uIWindow == null) return null;

            m_DicWindows.Add(windowUIType, uIWindow);
            //当前ui类型
            uIWindow.CurrentUIType = windowUIType;

            switch (uIWindow.WindowUIContainerType)
            {
                case WindowUIContainerType.Center:
                    obj.transform.SetParent(UISceneCtrl.Instance.CurUIScene.Center_Container);
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

            obj.SetActive(false);

            //打开动画
            StarWindowAnim(uIWindow, true);

        }
        else
        {
            obj = m_DicWindows[windowUIType].gameObject;
        }
        
        //设置层级
        LayerUIMgr.Instance.SetLayer(obj);
     
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
        StarWindowAnim(m_DicWindows[windowUIType], false);
    }

    #endregion

    #region 打开UI窗口动画 StartOpenAnim
    /// <summary>
    /// 打开UI窗口动画
    /// </summary>
    /// <param name="uIWindow"></param>
    /// <param name="isOpen"></param>
    private void StarWindowAnim(UIWindowBase uIWindow , bool isOpen)
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
                FromToAnim(uIWindow,0,isOpen);
                break;
            case WindowShowStyle.FromBottom:
                FromToAnim(uIWindow, 1, isOpen);
                break;
            case WindowShowStyle.FromLeft:
                FromToAnim(uIWindow, 2, isOpen);
                break;
            case WindowShowStyle.FromRight:
                FromToAnim(uIWindow, 3, isOpen);
                break;
        }
    }

    #endregion

    #region 窗口出现/关闭动画效果实现
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
        uIWindow.gameObject.SetActive(true);
        uIWindow.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        uIWindow.transform.DOScale(Vector3.one, uIWindow.Duration)
               .SetEase(GlobalInit.Instance.UIAnimCurve)
               .SetAutoKill(false)
               .OnRewind(
                () => DestoryWindowUI(uIWindow)
            );

        if (isOpen)
        {
            uIWindow.transform.DOPlayForward();
        }
        else
        {
            uIWindow.transform.DOPlayBackwards();
        }
    }

    /// <summary>
    /// 上下左右各个方向飞入
    /// </summary>
    /// <param name="uIWindow"></param>
    /// <param name="type">0表示从上边  1表示从下边 2表示从左边 3表示从右边</param>
    /// <param name="isOpen"></param>
    private void FromToAnim(UIWindowBase uIWindow, int type,bool isOpen)
    {
        Vector3 targetPos = Vector3.zero;
        switch (type)
        {
            case 0:
                targetPos = new Vector3(0, 1000, 0);
                break;
            case 1:
                targetPos = new Vector3(0, -1000, 0);
                break;
            case 2:
                targetPos = new Vector3(-1400, 0, 0);
                break;
            case 3:
                targetPos = new Vector3(1400, 0, 0);
                break;
        }
        uIWindow.gameObject.SetActive(true);
        uIWindow.transform.localPosition = Vector3.zero;
        uIWindow.transform.DOLocalMove(targetPos, uIWindow.Duration)
               .SetEase(GlobalInit.Instance.UIAnimCurve)
               .SetAutoKill(false)
               .OnRewind(
                () => DestoryWindowUI(uIWindow)
            );

        if (isOpen)
        {
            uIWindow.transform.DOPlayForward();
        }
        else
        {
            uIWindow.transform.DOPlayBackwards();
        }
    }
    #endregion

    #region 销毁窗口
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
    #endregion
}
