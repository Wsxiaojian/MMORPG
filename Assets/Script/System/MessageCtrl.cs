//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-14 10:47:45 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using UnityEngine;

public class MessageCtrl : Singleton<MessageCtrl>
{
    private UIMessageView m_UIMessageView;

    /// <summary>
    /// 打开提示信息显示
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="messageViewType"></param>
    /// <param name="onOk"></param>
    /// <param name="onCancel"></param>
    public void Show(string title, string content, MessageViewType messageViewType = MessageViewType.MVT_OK, Action onOk = null, Action onCancel = null)
    {
        if (m_UIMessageView == null)
        {
            //窗口命名规则需要统一 “ pan_ + 窗口类型枚举名称 ”
           GameObject obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindow, "pan_Message");
            if (obj != null)
            {
                obj.transform.SetParent(UISceneCtrl.Instance.CurUIScene.Center_Container);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localEulerAngles = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                m_UIMessageView = obj.GetOrCreateComponent<UIMessageView>();
            }
        }

        if (m_UIMessageView != null)
        {
            m_UIMessageView.Show(title, content, messageViewType, onOk, onCancel);
        }
    }

}
