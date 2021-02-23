//***********************************************************
// 描述：角色信息窗口
// 作者：fanwei 
// 创建时间：2021-02-23 21:10:05
// 版本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 角色信息窗口
/// </summary>
public class UIWindowRoleInfo : UIWindowBase
{


    protected override void OnBtnClick(Button btn)
    {
        base.OnBtnClick(btn);

        switch (btn.gameObject.name)
        {
            case "Btn_Close":
                BtnCloseClick();
                break;
        }
    }


    /// <summary>
    /// 关闭窗口
    /// </summary>
    private void BtnCloseClick()
    {
        Close();
    }
}
