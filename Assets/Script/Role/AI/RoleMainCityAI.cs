//***********************************************************
// 描述：主角主城AI逻辑
// 作者：fanwei 
// 创建时间：2021-02-26 06:41:36 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角主城AI逻辑
/// </summary>
public class RoleMainCityAI : IRoleAI
{
    public RoleCtrl CurRoleCtrl
    {
        get;
        set;
    }

    public RoleMainCityAI(RoleCtrl roleCtrl)
    {
        CurRoleCtrl = roleCtrl;
    }


    /// <summary>
    /// 角色AI处理逻辑
    /// </summary>
    public void DoAI()
    {


    }
}
