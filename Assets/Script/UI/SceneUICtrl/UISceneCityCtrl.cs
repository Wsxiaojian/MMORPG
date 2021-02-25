//***********************************************************
// 描述：主场场景UI控制
// 作者：fanwei 
// 创建时间：2021-02-23 20:13:15
// 版本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主场场景UI控制
/// </summary>
public class UISceneCityCtrl : UISceneBase
{
   
    protected override void OnBtnClick(Button btn)
    {
        base.OnBtnClick(btn);

        switch (btn.gameObject.name)
        {
            //左上  角色信息
            case "Btn_RoleIcon":
                BtnRoleIconClick();
                break;

            //右上 
            case "Btn_Shop":
                break;
            case "Btn_Recharge":
                break;
            case "Btn_Equip":
                break;

            //左下
            case "Btn_Backpack":
                break;
            case "Btn_Kungfu":
                break;
            case "Btn_Task":
                break;

            //右下
            case "Btn_Skill1":
                break;
            case "Btn_Skill2":
                break;
            case "Btn_Skill3":
                break;
            case "Btn_Blood":
                break;
        }
    }

    /// <summary>
    /// 打开角色信息
    /// </summary>
    private void BtnRoleIconClick()
    {
        //打开角色信息
        WindowUIMgr.Instance.OpenWindow(WindowUIType.RoleInfo);
    }
}
