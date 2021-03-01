//***********************************************************
// 描述：角色AI控制接口
// 作者：fanwei 
// 创建时间：2021-02-26 06:41:22 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色AI控制接口
/// </summary>
public interface IRoleAI
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    RoleCtrl CurRoleCtrl
    {
        get;
        set;
    }

    /// <summary>
    /// 角色AI处理
    /// </summary>
    void DoAI();
}
