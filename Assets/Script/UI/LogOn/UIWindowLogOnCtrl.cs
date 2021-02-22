//***********************************************************
// ����������һ�����ܴ���
// ���ߣ�fanwei 
// ����ʱ�䣺2021-02-23 07:16:58
// �� ����1.0
// ��ע��
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


    protected override void OnBtnClick(Button btn)
    {
        base.OnBtnClick(btn);

        switch (btn.name)
        {
            //登陆
            case "Btn_LogOn":
                break;
            // 注册窗口
            case "Btn_Reg":
                btn.onClick.AddListener(BtnRegClick);
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
}
