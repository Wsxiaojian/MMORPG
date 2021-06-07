//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-14 10:28:46 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ui提示信息
/// </summary>
public class UIMessageView : MonoBehaviour
{
    /// <summary>
    /// 提示标题
    /// </summary>
    private Text Txt_Title;
    /// <summary>
    /// 提示内容
    /// </summary>
    private Text Txt_Content;
    /// <summary>
    /// 确认按钮
    /// </summary>
    private Button Btn_Ok;
    /// <summary>
    /// 取消按钮
    /// </summary>
    private Button Btn_Cancel;

    /// <summary>
    /// 
    /// </summary>
    private RectTransform rectTransform;

    /// <summary>
    /// 点击Ok按钮
    /// </summary>
    private Action OnOkClickCallBack;
    /// <summary>
    /// 
    /// </summary>
    private Action OnCancelClickCallBack;


    private void Awake()
    {
        rectTransform = transform.GetComponent<RectTransform>();

        Txt_Title = transform.Find("Title/Txt_Title").GetComponent<Text>();
        Txt_Content = transform.Find("Txt_Content").GetComponent<Text>();
        Btn_Ok = transform.Find("Button/Btn_Ok").GetComponent<Button>();
        Btn_Cancel = transform.Find("Button/Btn_Cancel").GetComponent<Button>();

        EventTriggerListener.Get(Btn_Ok.gameObject).onClick = OnOkClick;
        EventTriggerListener.Get(Btn_Cancel.gameObject).onClick = OnCancelClick;
    }

    private void OnOkClick(GameObject go)
    {
        if (OnOkClickCallBack != null)
        {
            OnOkClickCallBack();
        }
        Close();
    }
    private void OnCancelClick(GameObject go)
    {
        if (OnCancelClickCallBack != null)
        {
            OnCancelClickCallBack();
        }
        Close();
    }

    private void Close()
    {
        rectTransform.localPosition = new Vector3(0, 5000, 0);
    }

    /// <summary>
    /// 打开提示信息显示
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="messageViewType"></param>
    /// <param name="onOk"></param>
    /// <param name="onCancel"></param>
    public void Show(string title,string content,MessageViewType messageViewType = MessageViewType.MVT_OK, Action onOk = null,Action onCancel =null)
    {
        rectTransform.localPosition = Vector3.zero;

        Txt_Title.text = title;
        Txt_Content.text = content;

        OnOkClickCallBack = onOk;
        OnCancelClickCallBack = onCancel;

        switch (messageViewType)
        {
            case MessageViewType.MVT_OK:
                Btn_Ok.gameObject.SetActive(true);
                Btn_Cancel.gameObject.SetActive(false);
                break;
            case MessageViewType.MVT_OkAndCancel:
                Btn_Ok.gameObject.SetActive(true);
                Btn_Cancel.gameObject.SetActive(true);
                break;
        }
    }
}
