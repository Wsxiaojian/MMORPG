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

    private float m_NextAttackTime;

    public RoleMainCityAI(RoleCtrl roleCtrl)
    {
        CurRoleCtrl = roleCtrl;
    }


    /// <summary>
    /// 角色AI处理逻辑
    /// </summary>
    public void DoAI()
    {
        if(CurRoleCtrl.LockEnemy != null)
        {
            if (CurRoleCtrl.LockEnemy.CurRoleInfo.CurHp < 0)
            {
                CurRoleCtrl.LockEnemy = null;
                return;
            }

            //攻击
            if(CurRoleCtrl.CurRoleFSMMgr.CurRoleStateType != RoleStateType.Attack && Time.time> m_NextAttackTime)
            {
                //每秒攻击一次
                m_NextAttackTime = Time.time + 1;
                CurRoleCtrl.DoAttack();
            }
        }
    }
}
