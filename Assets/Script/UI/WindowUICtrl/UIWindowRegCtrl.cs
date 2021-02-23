//***********************************************************
// 描述：注册窗口控制
// 作者：fanwei 
// 创建日期：2021-02-23 07:27:56
// 版本：1.0
// 备注
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 注册窗口控制
/// </summary>
public class UIWindowRegCtrl : UIWindowBase
{
    /// <summary>
    /// 呢称输入框
    /// </summary>
    [SerializeField]
    private InputField Inp_NickName;
    /// <summary>
    /// 密码输入框
    /// </summary>
    [SerializeField]
    private InputField Inp_Pwd;
    /// <summary>
    /// 确认密码输入框
    /// </summary>
    [SerializeField]
    private InputField Inp_SurePwd;

    /// <summary>
    /// 输入提示
    /// </summary>
    [SerializeField]
    private Text Txt_InputTip;


    protected override void OnStart()
    {
        base.OnStart();

        Txt_InputTip.text = string.Empty;
    }


    protected override void OnBtnClick(Button btn)
    {
        base.OnBtnClick(btn);

        switch (btn.name)
        {
            //返回登陆窗口
            case "Btn_LogOn":
                BtnLogOnClick();
                break;
            //注册
            case "Btn_Reg":
                BtnRegClick();
                break;
        }
    }


    /// <summary>
    /// 进入登陆窗口
    /// </summary>
    private void BtnLogOnClick()
    {
        NextWindowUIType = WindowUIType.LogOn;

        Close();
    }

    /// <summary>
    /// 注册
    /// </summary>
    private void BtnRegClick()
    {
        string nickName = Inp_NickName.text.Trim();
        string pwd = Inp_Pwd.text.Trim();
        string surePwd = Inp_SurePwd.text.Trim();

        if (string.IsNullOrEmpty(nickName))
        {
            //呢称不能为空
            Txt_InputTip.text = "呢称不能为空！";
            return;
        }
        if (string.IsNullOrEmpty(pwd))
        {
            //密码不能为空
            Txt_InputTip.text = "密码不能为空！";
            return;
        }
        if (string.IsNullOrEmpty(surePwd))
        {
            //确认密码不能为空
            Txt_InputTip.text = "确认密码不能为空！";
            return; 
        }

        if(pwd!= surePwd)
        {
            //两次密码输入不一致
            Txt_InputTip.text = "两次密码输入不一致！";
            return;
        }

        PlayerPrefs.SetString(GlobalInit.MMO_NICKNAME, nickName);
        PlayerPrefs.SetString(GlobalInit.MMO_PWD, pwd);

        //切换场景
        SceneMgr.Instance.LoadMainCity();
    }
}
