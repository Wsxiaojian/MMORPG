//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-02-26 06:39:37 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateRun : RoleStateAbstract
{
    /// <summary>
    /// 旋转速度
    /// </summary>
    private float m_RotateSpeed;
    /// <summary>
    /// 目标宣祖安
    /// </summary>
    private Quaternion m_TargetRotate;

    public RoleStateRun(RoleFSMMgr roleFsm) : base(roleFsm)
    {
    }


    public override void OnEnter()
    {
        base.OnEnter();

        RoleFSMMgr.RoleCtrl.Animator.SetBool(TransToName.ToRun.ToString(), true);

        m_RotateSpeed = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        AnimStateInfo = RoleFSMMgr.RoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if(AnimStateInfo.IsName(RoleAnimName.Run.ToString()))
        {
            RoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.CurState.ToString(), (int)RoleStateType.Run);

            if (Vector3.Distance(RoleFSMMgr.RoleCtrl.transform.position, RoleFSMMgr.RoleCtrl.TargetPos) > 0.1f)
            {
                //方向
                Vector3 direction = (RoleFSMMgr.RoleCtrl.TargetPos - RoleFSMMgr.RoleCtrl.transform.position).normalized;
                direction.y = 0f;
                direction = direction * Time.deltaTime * RoleFSMMgr.RoleCtrl.MoveSpeed;

                //转向
                if (m_RotateSpeed < 1)
                {
                    m_RotateSpeed += 5f * Time.deltaTime;
                    m_TargetRotate = Quaternion.LookRotation(direction);
                    RoleFSMMgr.RoleCtrl.transform.rotation = Quaternion.Lerp(RoleFSMMgr.RoleCtrl.transform.rotation, m_TargetRotate, m_RotateSpeed);
                }

                //移动
                RoleFSMMgr.RoleCtrl.CharacterController.Move(direction);
            }
            else
            {
                RoleFSMMgr.RoleCtrl.DoIdle();
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        RoleFSMMgr.RoleCtrl.Animator.SetBool(TransToName.ToRun.ToString(), false);
    }
}
