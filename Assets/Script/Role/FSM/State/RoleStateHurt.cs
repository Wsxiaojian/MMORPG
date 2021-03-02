//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-02-26 06:39:47 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateHurt : RoleStateAbstract
{
    public RoleStateHurt(RoleFSMMgr roleFsm) : base(roleFsm)
    {
    }


    public override void OnEnter()
    {
        base.OnEnter();

        CurRoleFSMMgr.RoleCtrl.Animator.SetBool(TransToName.ToHurt.ToString(), true);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        AnimStateInfo = CurRoleFSMMgr.RoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (AnimStateInfo.IsName(RoleAnimName.Hurt.ToString()))
        {
            CurRoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.CurState.ToString(), (int)RoleStateType.Hurt);

            //动画播完
            if (AnimStateInfo.normalizedTime > 1)
            {
                CurRoleFSMMgr.RoleCtrl.DoIdle();
            }
        }
        //else
        //{
        //    CurRoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.CurState.ToString(), (int)RoleStateType.Idle);
        //}
    }

    public override void OnExit()
    {
        base.OnExit();
        CurRoleFSMMgr.RoleCtrl.Animator.SetBool(TransToName.ToHurt.ToString(), false);
    }
}
