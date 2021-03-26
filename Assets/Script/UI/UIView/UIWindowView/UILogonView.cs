//***********************************************************
// 描述：登录窗口
// 作者：fanwei 
// 创建时间：2021-03-26 09:16:08 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 登录窗口
/// </summary>
public class UILogonView : UIWindowViewBase
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

    /// <summary>
    /// 按钮点击
    /// </summary>
    /// <param name="btnGo"></param>
    protected override void OnBtnClick(GameObject btnGo)
    {
        base.OnBtnClick(btnGo);

        switch (btnGo.name)
        {
            //登陆
            case "Btn_LogOn":

                break;
            // 注册窗口
            case "Btn_Reg":
                //切换到注册窗口
                break;
        }
    }

    /// <summary>
    /// 设置错误信息提示
    /// </summary>
    /// <param name="errorTip"></param>
    public void SetErrorTip(string errorTip)
    {
        Txt_InputTip.text = errorTip;
    }
}
