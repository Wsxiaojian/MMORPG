//***********************************************************
// ����������һ�����ܴ���
// ���ߣ�fanwei 
// ����ʱ�䣺2021-02-23 07:27:56
// �� ����1.0
// ��ע��
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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



    protected override void OnBtnClick(Button btn)
    {
        base.OnBtnClick(btn);

        switch (btn.name)
        {
            //返回登陆窗口
            case "Btn_LogOn":
                btn.onClick.AddListener(BtnLogOnClick);
                break;
            //注册
            case "Btn_Reg":
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
}
