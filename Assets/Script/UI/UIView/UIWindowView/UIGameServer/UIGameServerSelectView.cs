//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-14 20:07:06 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameServerSelectView : UIWindowViewBase
{
    #region 左边页卡属性
    /// <summary>
    /// item预制体
    /// </summary>
    [SerializeField]
    private GameObject GameServerPageItemPfb;

    /// <summary>
    /// pageItem 父物体
    /// </summary>
    [SerializeField]
    private Transform m_PageItemParent;

    /// <summary>
    /// 所有的区服页卡信息 
    ///     初始化时候 默认生成10个
    /// </summary>
    private List<GameObject> m_GameServerPageItemGoList;

    ///// <summary>
    ///// 点击页卡回调
    ///// </summary>
    //public Action<RetGameServerPageEntity> OnClickPageCallBack;
    #endregion


    #region 右边区服属性
    /// <summary>
    /// item预制体
    /// </summary>
    [SerializeField]
    private GameObject GameServerItemPfb;

    /// <summary>
    /// pageItem 父物体
    /// </summary>
    [SerializeField]
    private Transform m_ServerItemParent;

    /// <summary>
    /// 当前选中的服务器Item
    /// </summary>
    [SerializeField]
    private UIGameServerItemView m_CurSelectServerItemView;

    /// <summary>
    /// 所有的区服信息 
    ///     初始化时候 默认生成10个
    /// </summary>
    private List<GameObject> m_GameServerItemGoList;

    ///// <summary>
    ///// 按钮点击回调
    ///// </summary>
    //public Action<RetGameServerEntity> OnClickServerItemCallBack;
    #endregion

    private void Awake()
    {
        m_GameServerPageItemGoList = new List<GameObject>();

        m_GameServerItemGoList = new List<GameObject>();
        if (GameServerItemPfb != null)
        {
            GameObject obj = null;
            for (int i = 0; i < 10; i++)
            {
                obj = Instantiate(GameServerItemPfb, m_ServerItemParent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                obj.SetActive(false);

                m_GameServerItemGoList.Add(obj);
            }
        }
    }

    /// <summary>
    /// 设置服务器信息
    /// </summary>
    /// <param name="pageEntities"></param>
    public void SetGameServerPageUI(List<RetGameServerPageEntity> pageEntities)
    {
        int curCount = m_GameServerPageItemGoList.Count;

        GameObject obj = null;
        for (int i = 0; i < pageEntities.Count; i++)
        {
            if(i >= curCount)
            {
                obj = Instantiate(GameServerPageItemPfb, m_PageItemParent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                m_GameServerPageItemGoList.Add(obj);
            }
            else
            {
                obj = m_GameServerPageItemGoList[i];
                if (obj.activeSelf) obj.SetActive(false);
            }
          
            if (obj!= null)
            {
                UIGameServerPageItemView view =  obj.GetComponent<UIGameServerPageItemView>();
                view.SetUI(pageEntities[i]);
                //view.OnClickPageCallBack = OnClickPageCallBack;
            }
        }

        if(pageEntities.Count < m_GameServerPageItemGoList.Count)
        {
            for (int i = pageEntities.Count; i < m_GameServerPageItemGoList.Count; i++)
            {
                if (m_GameServerPageItemGoList[i].activeSelf) m_GameServerPageItemGoList[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 设置右边区服信息显示
    /// </summary>
    /// <param name="serverEntities"></param>
    public void SetGameServerUI(List<RetGameServerEntity> serverEntities)
    {
        GameObject obj = null;
        for (int i = 0; i < m_GameServerItemGoList.Count; i++)
        {
            obj = m_GameServerItemGoList[i];
            if (obj != null)
            {
                if (i < serverEntities.Count)
                {
                    if (obj.activeSelf ==false) obj.SetActive(true);

                    UIGameServerItemView view = obj.GetComponent<UIGameServerItemView>();
                    view.SetUI(serverEntities[i]);
                    //view.OnClickServerItemCallBack = OnClickServerItemCallBack;
                }
                else
                {
                    if (obj.activeSelf) obj.SetActive(false);
                }
            }
        }
    }


    /// <summary>
    /// 设置选择区服UI显示
    /// </summary>
    /// <param name="entity"></param>
    public void SetSelectGameServerUI(RetGameServerEntity entity)
    {
        m_CurSelectServerItemView.SetUI(entity);
    }
}
