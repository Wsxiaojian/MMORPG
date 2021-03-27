//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-27 06:19:28 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主城场景显示
/// </summary>
public class UISceneCityView : UISceneViewBase
{
    protected override void OnBtnClick(GameObject btnGo)
    {
        base.OnBtnClick(btnGo);

        switch (btnGo.name)
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
        UIViewUtil.Instance.OpenWindow(WindowUIType.RoleInfo);
    }
}
