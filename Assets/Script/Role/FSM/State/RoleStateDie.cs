//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-02-26 06:39:56 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateDie : RoleStateAbstract
{
    public RoleStateDie(RoleFSMMgr roleFsm) : base(roleFsm)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        RoleFSMMgr.RoleCtrl.Animator.SetBool(TransToName.ToDie.ToString(), true);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        AnimStateInfo = RoleFSMMgr.RoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (AnimStateInfo.IsName(RoleAnimName.Die.ToString()))
        {
            RoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.CurState.ToString(), (int)RoleStateType.Die);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        RoleFSMMgr.RoleCtrl.Animator.SetBool(TransToName.ToDie.ToString(), false);
    }
}
