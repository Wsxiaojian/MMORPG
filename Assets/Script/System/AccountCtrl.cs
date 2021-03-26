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


    public AccountCtrl()
    {
        //添加UI事件监听

    }

    public override void Dispose()
    {
        base.Dispose();
        //移除UI事件监听

    }

    //打开界面
    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.LogOn:
                
                break;
            case WindowUIType.Reg:
                break;
        }
    }

    private void OpenLogonView()
    {
        m_UILogonView = UIViewUtil.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogonView>();
        m_UILogonView.OpenNextWidow = OpenRegView;
    }
    private void OpenRegView()
    {
        m_UIRegView = UIViewUtil.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UIRegView>();
        m_UIRegView.OpenNextWidow = OpenLogonView;
    }



    //----------------------------UI事件回调----------------------------------------------
    /// <summary>
    /// 登陆
    /// </summary>
    private void UILogonView_BtnLogOnClick(params object[] pams)
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
    //----------------------------UI事件回调----------------------------------------------
}
