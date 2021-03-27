//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-26 09:17:25 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 账户相关控制
///     登录界面控制
///     注册界面控制
/// </summary>
public class AccountCtrl : Singleton<AccountCtrl>, ISystem
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
        UIDispatcher.Instance.AddListenter(ConstDef.UILogonView_Btn_LogOn, UILogonView_BtnLog_ClickCallBack);
        UIDispatcher.Instance.AddListenter(ConstDef.UILogonView_Btn_ToReg, UILogonView_Btn_ToReg_ClickCallBack);
        UIDispatcher.Instance.AddListenter(ConstDef.UIRegView_Btn_Reg, UIRegView_Btn_Reg_ClickCallBack);
        UIDispatcher.Instance.AddListenter(ConstDef.UIRegView_Btn_ToLogOn, UIRegView_Btn_ToLogOn_ClickCallBack);
    }

    public override void Dispose()
    {
        base.Dispose();
        //移除UI事件监听
        UIDispatcher.Instance.RemoveListenter(ConstDef.UILogonView_Btn_LogOn, UILogonView_BtnLog_ClickCallBack);
        UIDispatcher.Instance.RemoveListenter(ConstDef.UILogonView_Btn_ToReg, UILogonView_Btn_ToReg_ClickCallBack);
        UIDispatcher.Instance.RemoveListenter(ConstDef.UIRegView_Btn_Reg, UIRegView_Btn_Reg_ClickCallBack);
        UIDispatcher.Instance.RemoveListenter(ConstDef.UIRegView_Btn_ToLogOn, UIRegView_Btn_ToLogOn_ClickCallBack);
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
        m_UILogonView.OpenNextWidow = OpenRegView;
    }
    /// <summary>
    /// 打开注册界面
    /// </summary>
    private void OpenRegView()
    {
        m_UIRegView = UIViewUtil.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UIRegView>();
        m_UIRegView.OpenNextWidow = OpenLogonView;
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


        string oldNickName = PlayerPrefs.GetString(GlobalInit.MMO_NICKNAME);
        string oldpwd = PlayerPrefs.GetString(GlobalInit.MMO_PWD);


        if (nickName == oldNickName && pwd == oldpwd)
        {
            GlobalInit.Instance.CurNickName = nickName;

            //登陆成功  跳转主场景
            SceneMgr.Instance.LoadMainCity();
        }
        else
        {
            //呢称或密码错误
            m_UILogonView.SetErrorTip("呢称或密码输入错误！");
        }
    }
    /// <summary>
    /// 去注册 界面
    /// </summary>
    private void UILogonView_Btn_ToReg_ClickCallBack(params object[] pams)
    {
        m_UILogonView.Close(true);
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
            m_UIRegView.SetErrorTip("呢称不能为空！");
            return;
        }
        if (string.IsNullOrEmpty(pwd))
        {
            //密码不能为空
            m_UIRegView.SetErrorTip("确认密码不能为空！");
            return;
        }
        if (string.IsNullOrEmpty(surePwd))
        {
            //确认密码不能为空
            m_UIRegView.SetErrorTip("确认密码不能为空！");
            return;
        }

        if (pwd != surePwd)
        {
            //两次密码输入不一致
            m_UIRegView.SetErrorTip("两次密码输入不一致！");
            return;
        }

        PlayerPrefs.SetString(GlobalInit.MMO_NICKNAME, nickName);
        PlayerPrefs.SetString(GlobalInit.MMO_PWD, pwd);

        //当前玩家信息
        GlobalInit.Instance.CurNickName = nickName;

        //切换场景
        SceneMgr.Instance.LoadMainCity();
    }

    /// <summary>
    /// 去登录 界面
    /// </summary>
    private void UIRegView_Btn_ToLogOn_ClickCallBack(params object[] pams)
    {
        m_UIRegView.Close(true);
    }

    //----------------------------UI事件回调----------------------------------------------
}
