//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-14 20:05:21 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 设置区服信息
/// </summary>
public class UIGameServerPageItemView : MonoBehaviour
{
    /// <summary>
    /// 区服名称
    /// </summary>
    [SerializeField]
    private Text Txt_PageName;

    /// <summary>
    /// 当前item的实体信息
    /// </summary>
    private RetGameServerPageEntity m_CurEntity;

    ///// <summary>
    ///// 点击页卡回调
    ///// </summary>
    //public Action<RetGameServerPageEntity> OnClickPageCallBack;


    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnBtnClick);
    }

    /// <summary>
    /// 按钮点击
    /// </summary>
    private void OnBtnClick()
    {
        //if (OnClickPageCallBack != null)
        //{
        //    OnClickPageCallBack(m_CurEntity);
        //}
        UIDispatcher.Instance.Dispatch(ConstDef.UIGameServerSelectView_Btn_ClickPage,new object[] { m_CurEntity });
    }

    /// <summary>
    /// 设置UI显示信息
    /// </summary>
    /// <param name="entity"></param>
    public void SetUI(RetGameServerPageEntity entity)
    {
        m_CurEntity = entity;

        Txt_PageName.text = entity.Name;
    }
}
