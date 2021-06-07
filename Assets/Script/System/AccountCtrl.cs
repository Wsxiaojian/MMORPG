//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-26 09:17:25 
// 版本：1.0 
// 备注：
//***********************************************************
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 账户相关控制
///     登录界面控制
///     注册界面控制
/// </summary>
public class AccountCtrl : SystemBaseCtr<AccountCtrl>, ISystem
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    private UILogonView m_UILogonView;
    /// <summary>
    /// 注册窗口
    /// </summary>
    private UIRegView m_UIRegView;

    #region UIView 界面事件监听
    public AccountCtrl()
    {
        //添加UI事件监听
        AddListenter(ConstDef.UILogonView_Btn_LogOn, UILogonView_BtnLog_ClickCallBack);
        AddListenter(ConstDef.UILogonView_Btn_ToReg, UILogonView_Btn_ToReg_ClickCallBack);
        AddListenter(ConstDef.UIRegView_Btn_Reg, UIRegView_Btn_Reg_ClickCallBack);
        AddListenter(ConstDef.UIRegView_Btn_ToLogOn, UIRegView_Btn_ToLogOn_ClickCallBack);
    }

    public override void Dispose()
    {
        base.Dispose();
        //移除UI事件监听
        RemoveListenter(ConstDef.UILogonView_Btn_LogOn, UILogonView_BtnLog_ClickCallBack);
        RemoveListenter(ConstDef.UILogonView_Btn_ToReg, UILogonView_Btn_ToReg_ClickCallBack);
        RemoveListenter(ConstDef.UIRegView_Btn_Reg, UIRegView_Btn_Reg_ClickCallBack);
        RemoveListenter(ConstDef.UIRegView_Btn_ToLogOn, UIRegView_Btn_ToLogOn_ClickCallBack);
    }
    #endregion

    #region OpenView  打开 界面
    /// <summary>
    /// 打开 界面
    /// </summary>
    /// <param name="type"></param>
    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.LogOn:
                OpenLogonView();
                break;
            case WindowUIType.Reg:
                OpenRegView();
                break;
        }
    }

    /// <summary>
    /// 打开登录界面
    /// </summary>
    private void OpenLogonView()
    {
        m_UILogonView = UIViewUtil.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogonView>();
    }
    /// <summary>
    /// 打开注册界面
    /// </summary>
    private void OpenRegView()
    {
        m_UIRegView = UIViewUtil.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UIRegView>();
    }
    #endregion

    /// <summary>
    /// 自动登陆
    /// </summary>
    public void AutoLogon()
    {
        //1.存在账号 自动登陆
        if (PlayerPrefs.HasKey(ConstDef.GameServerAccountId))
        {
            //自动登陆
            string userName = PlayerPrefs.GetString(ConstDef.GameServerAccountUserName);
            string pwd = PlayerPrefs.GetString(ConstDef.GameServerAccountPwd);
            RequestLogonGameServer(userName, pwd);
        }
        //2.不存在  弹出登陆窗口
        else
        {
            OpenLogonView();
        }
    }

    # region 请求服务器
    private void RequestLogonGameServer(string userName, string pwd)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 1;
        dic["UserName"] = userName;
        dic["Pwd"] = pwd;

        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/Account", LogonCallBack, isPost: true, dic: dic);
    }
    /// <summary>
    /// 登陆后台回调
    /// </summary>
    /// <param name="obj"></param>
    private void LogonCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError == true)
        {
            AppLog.Log(obj.ErrorMsg);
        }
        else
        {
            //登陆成功
            RetValue ret = JsonMapper.ToObject<RetValue>(obj.Data);
            if (ret.HasError == false)
            {

               RetAccountEntity retAccountEntity = JsonMapper.ToObject<RetAccountEntity>(ret.Value);

                PlayerPrefs.SetInt(ConstDef.GameServerAccountId, retAccountEntity.Id);
                PlayerPrefs.SetString(ConstDef.GameServerAccountUserName, retAccountEntity.UserName);
                PlayerPrefs.SetString(ConstDef.GameServerAccountPwd, retAccountEntity.Pwd);

                GlobalInit.Instance.CurAccountEntity = retAccountEntity;

                //进入游戏服 进入 界面
                if (m_UILogonView != null)
                {
                    m_UILogonView.Close(WindowUIType.GameServerEnter);
                }
                else
                {
                    UIViewMgr.Instance.OpenView(WindowUIType.GameServerEnter);
                }
            }
            else
            {
                AppLog.Log(ret.ErrorMsg);
                OpenLogonView();
            }
        }
    }
    
    /// <summary>
    /// 请求 注册账号
    /// </summary>
    private void RequestRegisterAccount(string userName,string pwd,string channelId)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 0;
        dic["UserName"] = userName;
        dic["Pwd"] = pwd;
        dic["ChannelId"] = channelId;
       
        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/Account", RegisterCallBack, isPost: true, dic: dic);
    }
    /// <summary>
    /// 注册后台回调
    /// </summary>
    /// <param name="obj"></param>
    private void RegisterCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError == true)
        {
            AppLog.Log(obj.ErrorMsg);
        }
        else
        {
            //登陆成功
            RetValue ret = JsonMapper.ToObject<RetValue>(obj.Data);
            if (ret.HasError == false)
            {

                RetAccountEntity retAccountEntity = JsonMapper.ToObject<RetAccountEntity>(ret.Value);

                PlayerPrefs.SetInt(ConstDef.GameServerAccountId , retAccountEntity.Id);
                PlayerPrefs.SetString(ConstDef.GameServerAccountUserName, retAccountEntity.UserName);
                PlayerPrefs.SetString(ConstDef.GameServerAccountPwd , retAccountEntity.Pwd);

                GlobalInit.Instance.CurAccountEntity = retAccountEntity;

                //进入游戏服 进入 界面
                if (m_UIRegView != null)
                {
                    m_UIRegView.Close(WindowUIType.GameServerEnter);
                }
                else
                {
                    UIViewMgr.Instance.OpenView(WindowUIType.GameServerEnter);
                }
            }
            else
            {
                AppLog.Log(ret.ErrorMsg);
            }
        }
    }
    #endregion


    //----------------------------UI事件回调----------------------------------------------

    /// <summary>
    /// 登陆
    /// </summary>
    private void UILogonView_BtnLog_ClickCallBack(params object[] pams)
    {
        string nickName = pams[0].ToString();
        string pwd = pams[1].ToString();

        RequestLogonGameServer(nickName, pwd);
    }

    /// <summary>
    /// 去注册 界面
    /// </summary>
    private void UILogonView_Btn_ToReg_ClickCallBack(params object[] pams)
    {
        m_UILogonView.Close(WindowUIType.Reg);
    }

    /// <summary>
    /// 注册
    /// </summary>
    private void UIRegView_Btn_Reg_ClickCallBack(params object[] pams)
    {
        string nickName = pams[0].ToString();
        string pwd = pams[1].ToString();
        string surePwd = pams[2].ToString();

        if (string.IsNullOrEmpty(nickName))
        {
            //呢称不能为空
            ShowMessage("注册提示", "呢称不能为空！");
            return;
        }
        if (string.IsNullOrEmpty(pwd))
        {
            //密码不能为空
            ShowMessage("注册提示", "确认密码不能为空！");
            return;
        }
        if (string.IsNullOrEmpty(surePwd))
        {
            //确认密码不能为空
            ShowMessage("注册提示", "确认密码不能为空！");
            return;
        }

        if (pwd != surePwd)
        {
            //两次密码输入不一致
            ShowMessage("注册提示", "两次密码输入不一致！");
            return;
        }

        //请求服务器 注册账号
        RequestRegisterAccount(nickName, pwd, "1");
    }

    /// <summary>
    /// 去登录 界面
    /// </summary>
    private void UIRegView_Btn_ToLogOn_ClickCallBack(params object[] pams)
    {
        m_UIRegView.Close(WindowUIType.LogOn);
    }

    //----------------------------UI事件回调----------------------------------------------
}
