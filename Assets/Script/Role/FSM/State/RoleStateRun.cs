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

        CurRoleFSMMgr.RoleCtrl.Animator.SetBool(TransToName.ToRun.ToString(), true);

        m_RotateSpeed = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        AnimStateInfo = CurRoleFSMMgr.RoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if(AnimStateInfo.IsName(RoleAnimName.Run.ToString()))
        {
            CurRoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.CurState.ToString(), (int)RoleStateType.Run);
        }
        else
        {
            CurRoleFSMMgr.RoleCtrl.Animator.SetInteger(TransToName.CurState.ToString(), (int)RoleStateType.None);
        }


        Vector3 targetPos = new Vector3(CurRoleFSMMgr.RoleCtrl.TargetPos.x, CurRoleFSMMgr.RoleCtrl.transform.position.y, CurRoleFSMMgr.RoleCtrl.TargetPos.z);
        if (Vector3.Distance(CurRoleFSMMgr.RoleCtrl.transform.position, targetPos) > 0.1f)
        {
            //方向
            Vector3 direction = CurRoleFSMMgr.RoleCtrl.TargetPos - CurRoleFSMMgr.RoleCtrl.transform.position;
            direction = direction.normalized;
            direction = direction * Time.deltaTime * CurRoleFSMMgr.RoleCtrl.MoveSpeed;
            direction.y = 0f;

            //转向
            if (m_RotateSpeed < 1)
            {
                m_RotateSpeed += 5f * Time.deltaTime;
                m_TargetRotate = Quaternion.LookRotation(direction);
                CurRoleFSMMgr.RoleCtrl.transform.rotation = Quaternion.Lerp(CurRoleFSMMgr.RoleCtrl.transform.rotation, m_TargetRotate, m_RotateSpeed);
            }

            if (Quaternion.Angle(CurRoleFSMMgr.RoleCtrl.transform.rotation, m_TargetRotate) < 1)
            {
                m_RotateSpeed = 0;
            }

            //移动
            CurRoleFSMMgr.RoleCtrl.CharacterController.Move(direction);
        }
        else
        {
            CurRoleFSMMgr.RoleCtrl.DoIdle();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        CurRoleFSMMgr.RoleCtrl.Animator.SetBool(TransToName.ToRun.ToString(), false);
    }
}
