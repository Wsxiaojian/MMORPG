//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-26 09:17:00 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 注册窗口
/// </summary>
public class UIRegView : UIWindowViewBase
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

    protected override void OnBtnClick(GameObject btnGo)
    {
        base.OnBtnClick(btnGo);
        switch (btnGo.name)
        {
            //返回登陆窗口
            case "Btn_ToLogOn":
                UIDispatcher.Instance.Dispatch(ConstDef.UIRegView_Btn_ToLogOn);
                break;
            //注册
            case "Btn_Reg":
                UIDispatcher.Instance.Dispatch(ConstDef.UIRegView_Btn_Reg,
                    new string[] { Inp_NickName.text.Trim(), Inp_Pwd.text.Trim(), Inp_SurePwd.text.Trim()});
                break;
        }
    }

    ///// <summary>
    ///// 设置错误信息提示
    ///// </summary>
    ///// <param name="errorTip"></param>
    //public void SetErrorTip(string errorTip)
    //{
    //    Txt_InputTip.text = errorTip;
    //}
}
