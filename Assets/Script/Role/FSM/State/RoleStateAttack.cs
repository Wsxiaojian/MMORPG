//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-02-26 06:40:12 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateAttack : RoleStateAbstract
{
    public RoleStateAttack(RoleFSMMgr roleFsm) : base(roleFsm)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        CurRoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.ToPhyAttack.ToString(), 1);
        if(CurRoleFSMMgr.RoleCtrl.LockEnemy != null)
        {
            CurRoleFSMMgr.RoleCtrl.transform.LookAt(new Vector3(CurRoleFSMMgr.RoleCtrl.LockEnemy.transform.position.x, CurRoleFSMMgr.RoleCtrl.transform.position.y, CurRoleFSMMgr.RoleCtrl.LockEnemy.transform.position.z));
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        AnimStateInfo = CurRoleFSMMgr.RoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (AnimStateInfo.IsName(RoleAnimName.PhyAttack1.ToString()))
        {
            CurRoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.CurState.ToString(), (int)RoleStateType.Attack);

            //动画播完
            if (AnimStateInfo.normalizedTime >1)
            {
                CurRoleFSMMgr.RoleCtrl.DoIdle();
            }
        }
        else
        {
            CurRoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.CurState.ToString(), (int)RoleStateType.None);
        }

    }

    public override void OnExit()
    {
        base.OnExit();
        CurRoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.ToPhyAttack.ToString(), 0);
    }
}