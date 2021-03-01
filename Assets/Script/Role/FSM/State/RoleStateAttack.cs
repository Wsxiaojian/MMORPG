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

        RoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.ToPhyAttack.ToString(), 1);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        AnimStateInfo = RoleFSMMgr.RoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (AnimStateInfo.IsName(RoleAnimName.PhyAttack1.ToString()))
        {
            RoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.CurState.ToString(), (int)RoleStateType.Attack);

            //动画播完
            if (AnimStateInfo.normalizedTime >1)
            {
                RoleFSMMgr.RoleCtrl.DoIdle();
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        RoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.ToPhyAttack.ToString(), 0);
    }
}