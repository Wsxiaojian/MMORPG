//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-14 20:25:03 
// 版本：1.0 
// 备注：
//***********************************************************
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 区服选择控制
/// </summary>
public class GameServerCtrl : SystemBaseCtr<GameServerCtrl>, ISystem
{
    #region UI 属性对象
    /// <summary>
    /// 游戏服务进入窗口
    /// </summary>
    private UIGameServerEnterView m_GameServerEnterView;
    /// <summary>
    ///  游戏服务选择窗口
    /// </summary>
    private UIGameServerSelectView m_GameServerSelectView;
    #endregion

    #region UIView 界面事件监听
    public GameServerCtrl()
    {
        //添加UI事件监听
        AddListenter(ConstDef.UIGameServerEnterView_Btn_SelectServer, UIGameServerEnterView_Btn_SelectServerCallBack);
        AddListenter(ConstDef.UIGameServerEnterView_Btn_EnterGame, UIGameServerEnterView_Btn_EnterGameCallBack);

        AddListenter(ConstDef.UIGameServerSelectView_Btn_ClickPage, UIGameServerSelectView_Btn_ClickPageCallBack);
        AddListenter(ConstDef.UIGameServerSelectView_Btn_ClickServer, UIGameServerSelectView_Btn_ClickServerCallBack);
    }

    public override void Dispose()
    {
        base.Dispose();
        //移除UI事件监听
        RemoveListenter(ConstDef.UIGameServerEnterView_Btn_SelectServer, UIGameServerEnterView_Btn_EnterGameCallBack);
        RemoveListenter(ConstDef.UIGameServerEnterView_Btn_EnterGame, UIGameServerEnterView_Btn_SelectServerCallBack);

        RemoveListenter(ConstDef.UIGameServerSelectView_Btn_ClickPage, UIGameServerSelectView_Btn_ClickPageCallBack);
        RemoveListenter(ConstDef.UIGameServerSelectView_Btn_ClickServer, UIGameServerSelectView_Btn_ClickServerCallBack);
    }
    #endregion

    #region OpenView 打开页面
    /// <summary>
    /// 打开页面
    /// </summary>
    /// <param name="type"></param>
    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.GameServerEnter:
                OpenGameServerEnterView();
                break;
            case WindowUIType.GameServerSelect:
                OpenGameServerSelectView();
                break;
        }
    }

    /// <summary>
    /// 打开游戏服进入界面
    /// </summary>
    private void OpenGameServerEnterView()
    {
        m_GameServerEnterView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameServerEnter).GetComponent<UIGameServerEnterView>();
        if (m_GameServerEnterView != null)
        {
            m_GameServerEnterView.OnShowCallBack = GameServerEnterViewShowCallBack;
        }
    }

    /// <summary>
    /// 进入服务器
    /// </summary>
    private void GameServerEnterViewShowCallBack()
    {
        m_GameServerEnterView.SetUI(GlobalInit.Instance.CurAccountEntity.LastLogOnServerName);
    }


    /// <summary>
    /// 打开游戏服选择页面
    /// </summary>
    private void OpenGameServerSelectView()
    {
        m_GameServerSelectView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameServerSelect).GetComponent<UIGameServerSelectView>();
        if (m_GameServerSelectView != null)
        {
            m_GameServerSelectView.OnShowCallBack = OnGameServerSelectViewShowCallBack;
        }
    }
    /// <summary>
    /// 游戏服选择页面打开时回调
    ///     请求服务器 区服数据
    /// </summary>
    private void OnGameServerSelectViewShowCallBack()
    {
        //发送消息给服务器
        RequestGameServerPageInfo();


        //显示当前选择的服务器信息
        RetGameServerEntity retGameServerEntity = new RetGameServerEntity();
        retGameServerEntity.Id = GlobalInit.Instance.CurAccountEntity.LastLogonServerId;
        retGameServerEntity.Name = GlobalInit.Instance.CurAccountEntity.LastLogOnServerName;
        retGameServerEntity.ServerStatus = 1;
        retGameServerEntity.Ip = GlobalInit.Instance.CurAccountEntity.LastLogOnServerIp;
        retGameServerEntity.Port = GlobalInit.Instance.CurAccountEntity.LastLogOnServerPort;
        m_GameServerSelectView.SetSelectGameServerUI(retGameServerEntity);
    }
    #endregion

    #region RequestGameServer 请求服务器 数据
    /// <summary>
    /// 向服务器请求游戏区服列表信息
    /// type =0
    /// </summary>
    private void RequestGameServerPageInfo()
    {
        //发送消息给服务器
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 0;

        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", ReceiveGameServerPageInfoCallBack, isPost: true, dic: dic);
    }
    /// <summary>
    ///  接受服务器区服信息 回调
    /// </summary>
    /// <param name="obj"></param>
    private void ReceiveGameServerPageInfoCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError == true)
        {
            AppLog.Log(obj.ErrorMsg);
        }
        else
        {
            RetValue ret = JsonMapper.ToObject<RetValue>(obj.Data);

            if (ret.HasError == false)
            {

                List<RetGameServerPageEntity> serverPageEntities = JsonMapper.ToObject<List<RetGameServerPageEntity>>(ret.Value);

                if (m_GameServerSelectView != null)
                {
                    RetGameServerPageEntity entity = new RetGameServerPageEntity();
                    entity.Name = "推荐区服";
                    entity.PageIndex = 0;
                    //插入第一个推荐服
                    serverPageEntities.Insert(0, entity);

                    m_GameServerSelectView.SetGameServerPageUI(serverPageEntities);

                    //再请求 最后登陆信息
                    //发送消息给服务器
                    RequestGameServerInfo(0);
                }
            }
            else
            {
                AppLog.Log(ret.ErrorMsg);
            }
        }
    }

    /// <summary>
    /// 向服务器请求 游戏区服信息
    /// type =1
    /// </summary>
    /// <param name="pageIndex">0表示推荐服信息</param>
    private void RequestGameServerInfo(int pageIndex)
    {
        //发送消息给服务器
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 1;
        dic["PageIndex"] = pageIndex;

        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", ReceiveGameServerInfoCallBack, isPost: true, dic: dic);
    }
    /// <summary>
    /// 接受服务器消息  回调
    /// </summary>
    /// <param name="obj"></param>
    private void ReceiveGameServerInfoCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError == true)
        {
            AppLog.Log(obj.ErrorMsg);
        }
        else
        {
            RetValue ret = JsonMapper.ToObject<RetValue>(obj.Data);

            if (ret.HasError == false)
            {

                List<RetGameServerEntity> serverEntities = JsonMapper.ToObject<List<RetGameServerEntity>>(ret.Value);

                if (m_GameServerSelectView != null)
                {
                    m_GameServerSelectView.SetGameServerUI(serverEntities);
                }
            }
            else
            {
                AppLog.Log(ret.ErrorMsg);
            }
        }

    }

    /// <summary>
    /// 请求 登陆游戏区服
    /// type =2
    /// </summary>
    /// <param name="gameServerId"></param>
    private void RequestLogOnGameServer(int gameServerId)
    {
        //发送消息给服务器
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 2;
        dic["GameServerId"] = gameServerId;

        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", ReceiveSetLastGameServerCallBack, isPost: true, dic: dic);
    }

    /// <summary>
    /// 接受登陆服务器
    /// </summary>
    /// <param name="obj"></param>
    private void ReceiveSetLastGameServerCallBack(NetWorkHttp.CallBackArgs obj)
    {
        //请求登陆服务器
    }
    #endregion

    //----------------------------UI事件回调----------------------------------------------
    #region   UIGameServerEnterView  进入游戏服务器
    /// <summary>
    /// 进入游戏
    /// </summary>
    /// <param name="parms"></param>
    private void UIGameServerEnterView_Btn_EnterGameCallBack(object[] parms)
    {

    }
    /// <summary>
    /// 选择区服按钮点击
    /// </summary>
    /// <param name="parms"></param>
    private void UIGameServerEnterView_Btn_SelectServerCallBack(object[] parms)
    {
        OpenGameServerSelectView();
    }
    #endregion

    # region UIGameServerSelectView  区服选择界面
    /// <summary>
    /// 进入游戏
    /// </summary>
    /// <param name="parms"></param>
    private void UIGameServerSelectView_Btn_ClickPageCallBack(object[] parms)
    {
        RetGameServerPageEntity entity = parms[0] as RetGameServerPageEntity;

        if (entity != null)
        {
            //请求服务器 单个区服信息
            RequestGameServerInfo(entity.PageIndex);
        }
    }
    /// <summary>
    /// 选择区服按钮点击
    /// </summary>
    /// <param name="parms"></param>
    private void UIGameServerSelectView_Btn_ClickServerCallBack(object[] parms)
    {
       RetGameServerEntity entity = parms[0] as RetGameServerEntity;

        if (m_GameServerSelectView != null)
        {
            m_GameServerSelectView.SetSelectGameServerUI(entity);
        }
        if (m_GameServerEnterView != null)
        {
            m_GameServerEnterView.SetUI(entity.Name);
        }
    }
    #endregion
    //----------------------------UI事件回调----------------------------------------------
}
