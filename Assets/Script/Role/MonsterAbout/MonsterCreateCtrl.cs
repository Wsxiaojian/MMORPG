//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-02 06:13:34 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出怪控制
/// </summary>
public class MonsterCreateCtrl : MonoBehaviour
{

    /// <summary>
    /// 当前出怪名字
    /// </summary>
    [SerializeField]
    private string MonsterNmae;

    /// <summary>
    /// 出怪最大数量
    /// </summary>
    [SerializeField]
    private int m_CreateMaxNum;

    /// <summary>
    /// 当前怪物数量
    /// </summary>
    private int m_CurMonsterNum;

    /// <summary>
    /// 下一出怪时间
    /// </summary>
    private float m_NextCreateTime;


    private void Awake()
    {
        m_NextCreateTime = Time.time;
        m_CurMonsterNum = 0;
    }


    private void Update()
    {
        if(Time.time > m_NextCreateTime && m_CurMonsterNum< m_CreateMaxNum)
        {
            m_NextCreateTime = Time.time + UnityEngine.Random.Range(3f, 6f);

            //创建怪物
            GameObject monsterGo = RoleMgr.Instance.LoadRole(RoleType.Monster, MonsterNmae);
            if (monsterGo != null)
            {
                //当前数量增加
                m_CurMonsterNum++;

                monsterGo.transform.SetParent(transform);
                //随机一个点
                monsterGo.transform.position = transform.TransformPoint(UnityEngine.Random.Range(-0.5f, 0.5f), 0, UnityEngine.Random.Range(-0.5f, 0.5f));

                RoleCtrl monsterRoleCtrl = monsterGo.GetComponent<RoleCtrl>();

                RoleInfoMonster roleInfo = new RoleInfoMonster();
                roleInfo.RoleServerID = DateTime.Now.Ticks;
                roleInfo.RoleID = 1;
                roleInfo.CurHp = roleInfo.HpMax = 100;
                roleInfo.RoleNickName = "偷书盗贼";

                monsterRoleCtrl.Init(RoleType.Monster, roleInfo, new RoleMonsterAI(monsterRoleCtrl));

                monsterRoleCtrl.OnRoleDie = MonsterRoleDie;
            }
        }
    }

    private void MonsterRoleDie(RoleCtrl roleCtrl)
    {
        m_CurMonsterNum--;
        Destroy(roleCtrl.gameObject);
    }
}
