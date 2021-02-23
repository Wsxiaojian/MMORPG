//***********************************************************
// 描述：窗口UI  登陆窗口
// 作者： fanwei 
// 创建日期：2021-02-23 07:16:58
// 版本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 窗口UI  登陆窗口
/// </summary>
public class UIWindowLogOnCtrl : UIWindowBase
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
            //登陆
            case "Btn_LogOn":
                BtnLogOnClick();
                break;
            // 注册窗口
            case "Btn_Reg":
                BtnRegClick();
                break;
        }
    }

    /// <summary>
    /// 进入注册窗口
    /// </summary>
    private void BtnRegClick()
    {
        NextWindowUIType = WindowUIType.Reg;

        Close();
    }

    /// <summary>
    /// 登陆
    /// </summary>
    private void BtnLogOnClick()
    {
        string nickName = Inp_NickName.text.Trim();
        string pwd = Inp_Pwd.text.Trim();


        string oldNickName = PlayerPrefs.GetString(GlobalInit.MMO_NICKNAME);
        string oldpwd = PlayerPrefs.GetString(GlobalInit.MMO_PWD);
        

        if(nickName == oldNickName && pwd == oldpwd)
        {
            //登陆成功  跳转主场景
            SceneMgr.Instance.LoadMainCity();
        }
        else
        {
            //呢称或密码错误
            Txt_InputTip.text = "呢称或密码输入错误！";
        }
    }
}
