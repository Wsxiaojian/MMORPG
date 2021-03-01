//***********************************************************
// 描述：小怪AI控制器
// 作者：fanwei 
// 创建时间：2021-02-26 06:41:46 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小怪AI控制器
/// </summary>
public class RoleMonsterAI : IRoleAI
{
    /// <summary>
    /// 角色
    /// </summary>
    public RoleCtrl CurRoleCtrl
    {
        get;
        set;
    }

    /// <summary>
    /// 巡逻范围
    /// </summary>
    private float m_PatrolDis = 3f;

    /// <summary>
    /// 下一次巡逻时间
    /// </summary>
    private float m_NextPatrolTime;


    /// <summary>
    /// 追击范围
    /// </summary>
    private float m_ChaseDis = 5f;

    /// <summary>
    /// 攻击范围
    /// </summary>
    private float m_AttackDis =1f;

    /// <summary>
    /// 下一攻击时间
    /// </summary>
    private float m_NextAttackTime;



    public RoleMonsterAI(RoleCtrl roleCtrl)
    {
        CurRoleCtrl = roleCtrl;
    }


    /// <summary>
    /// 角色AI逻辑处理
    /// </summary>
    public void DoAI()
    {
        //巡逻 找主角进行攻击 脱离追击范围 则小怪切换待机
        float distance = Vector3.Distance(CurRoleCtrl.transform.position, GlobalInit.Instance.CurPlayer.transform.position);
        if(distance > m_ChaseDis)
        {
            //超过追击范围
            if (CurRoleCtrl.CurRoleFSMMgr.CurRoleStateType == RoleStateType.Idle)
            {
                if (Time.deltaTime > m_NextPatrolTime)
                {
                    //切换巡逻处理
                    m_NextPatrolTime = Time.deltaTime + Random.Range(6, 10);

                    //先随机一个点
                    Vector3 targetPos = CurRoleCtrl.transform.parent.position + new Vector3(Random.Range(-m_PatrolDis, m_PatrolDis), 0, Random.Range(-m_PatrolDis, m_PatrolDis));

                    //切换至移动状态
                    CurRoleCtrl.DoMove(targetPos);
                }
            }
        }
        else 
        {
            //攻击范围
            if (distance > m_AttackDis)
            {
                //切换至移动状态
                CurRoleCtrl.DoMove(GlobalInit.Instance.CurPlayer.transform.position);
            }
            else 
            {
                if(Time.time > m_NextAttackTime)
                {
                    m_NextAttackTime = Time.time + Random.Range(3, 5);

                    //攻击
                    CurRoleCtrl.DoAttack();
                    int damage = Random.Range(50, 100);
                    GlobalInit.Instance.CurPlayer.DoHurt(damage);
                }
            }
        }

    }
}
