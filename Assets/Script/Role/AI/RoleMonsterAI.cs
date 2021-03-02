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
    /// 下一次巡逻时间
    /// </summary>
    private float m_NextPatrolTime;

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
        //死亡退出
        if (CurRoleCtrl.CurRoleFSMMgr.CurRoleStateType == RoleStateType.Die) return;

        if (CurRoleCtrl.LockEnemy == null)
        {
            //巡逻 找主角进行攻击 脱离追击范围 则小怪切换待机
            float distance = Vector3.Distance(CurRoleCtrl.transform.position, GlobalInit.Instance.CurPlayer.transform.position);
            //巡逻
            if (distance < CurRoleCtrl.ViewRange)
            {
                if(CurRoleCtrl.CurRoleFSMMgr.CurRoleStateType == RoleStateType.Idle)
                {
                    if (Time.time > m_NextPatrolTime)
                    {
                        //切换巡逻处理
                        m_NextPatrolTime = Time.time + Random.Range(6, 10);

                        //先随机一个点
                        Vector3 targetPos = CurRoleCtrl.transform.parent.position + 
                            new Vector3(Random.Range(-CurRoleCtrl.PatrolRange, CurRoleCtrl.PatrolRange), 0, Random.Range(-CurRoleCtrl.PatrolRange,CurRoleCtrl.PatrolRange));

                        //切换至移动状态
                        CurRoleCtrl.DoMove(targetPos);
                    }
                }
            }
            else
            {
                CurRoleCtrl.LockEnemy = GlobalInit.Instance.CurPlayer;
            }
        }
        else
        {
            if(CurRoleCtrl.LockEnemy.CurRoleInfo.CurHp<0)
            {
                CurRoleCtrl.LockEnemy = null;
                return;
            }

            //巡逻 找主角进行攻击 脱离追击范围 则小怪切换待机
                float distance = Vector3.Distance(CurRoleCtrl.transform.position, CurRoleCtrl.LockEnemy.transform.position);
            //大于可视范围  放弃目标
            if (distance > CurRoleCtrl.ViewRange)
            {
                CurRoleCtrl.LockEnemy = null;
            }
            //大于攻击距离  追击目标
            else if(distance > CurRoleCtrl.AttackRange)
            {
                if (CurRoleCtrl.CurRoleFSMMgr.CurRoleStateType == RoleStateType.Idle)
                {
                    //随机一个位置
                    Vector3 playerPos = Random.onUnitSphere * CurRoleCtrl.AttackRange;
                    playerPos.y = 0;
                    playerPos += CurRoleCtrl.LockEnemy.transform.position;

                    //切换至移动状态
                    CurRoleCtrl.DoMove(playerPos);
                }
            }
            else
            {
                if (CurRoleCtrl.CurRoleFSMMgr.CurRoleStateType != RoleStateType.Attack && Time.time > m_NextAttackTime)
                {
                    m_NextAttackTime = Time.time + Random.Range(3, 5);

                    //攻击
                    CurRoleCtrl.DoAttack();
                }
            }
        }

    }
}
