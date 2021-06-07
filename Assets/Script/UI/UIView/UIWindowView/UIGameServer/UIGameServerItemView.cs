//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-14 20:07:24 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 区服选择Item页面
/// </summary>
public class UIGameServerItemView : MonoBehaviour
{

    /// <summary>
    /// 服务器状态显示图片
    /// </summary>
    [SerializeField]
    private Sprite[] m_ServerStatus;

    /// <summary>
    /// 服务器状态显示图片
    /// </summary>
    [SerializeField]
    private Image Img_ServerStatus;

    /// <summary>
    /// 服务器名称
    /// </summary>
    [SerializeField]
    private Text Txt_ServerName;

    /// <summary>
    /// 当前item显示数据
    /// </summary>
    private RetGameServerEntity m_CurEntity;

    ///// <summary>
    ///// 按钮点击回调
    ///// </summary>
    //public Action<RetGameServerEntity> OnClickServerItemCallBack;


    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnBtnClick);
    }


    /// <summary>
    /// 按钮点击
    /// </summary>
    private void OnBtnClick()
    {
        //if (OnClickServerItemCallBack != null)
        //{
        //    OnClickServerItemCallBack(m_CurEntity);
        //}
        UIDispatcher.Instance.Dispatch(ConstDef.UIGameServerSelectView_Btn_ClickServer, new object[] { m_CurEntity });
    }

    /// <summary>
    /// 设置item显示
    /// </summary>
    /// <param name="entity"></param>
    public void SetUI(RetGameServerEntity entity)
    {
        m_CurEntity = entity;

        Img_ServerStatus.overrideSprite = m_ServerStatus[entity.ServerStatus];

        Txt_ServerName.text = entity.Name;
    }
}
