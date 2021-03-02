//***********************************************************
// 描述：角色控制
// 作者：fanwei 
// 创建时间：2021-02-13 12:01:20
// 版 本：1.0
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RoleCtrl : MonoBehaviour
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [HideInInspector]
    public CharacterController CharacterController;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed;
    /// <summary>
    /// 目标点
    /// </summary>
    [HideInInspector]
    public Vector3 TargetPos;

    /// <summary>
    /// 出生点
    /// </summary>
    [HideInInspector]
    public Vector3 BornPos;

    /// <summary>
    /// 锁定的敌人
    /// </summary>
    [HideInInspector]
    public RoleCtrl LockEnemy;

    /// <summary>
    /// 视野范围
    /// </summary>
    public float ViewRange = 8f;

    /// <summary>
    /// 巡逻范围
    /// </summary>
    public float PatrolRange = 3f;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float AttackRange = 2f;

    /// <summary>
    /// 动画机
    /// </summary>
    public Animator Animator;

    /// <summary>
    /// 角色状态机
    /// </summary>
    [HideInInspector]
    public RoleFSMMgr CurRoleFSMMgr;
    /// <summary>
    /// 角色Ai控制
    /// </summary>
    private IRoleAI m_CurRoleAI;
    /// <summary>
    /// 角色信息
    /// </summary>
    [HideInInspector]
    public RoleInfo CurRoleInfo;
    /// <summary>
    /// 角色类型
    /// </summary>
    public RoleType CurRoleType = RoleType.None;


    /// <summary>
    /// 头顶点
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarPos;
    /// <summary>
    /// 头顶Bar显示控制
    /// </summary>
    private RoleHeadBarCtrl m_RoleHeadBarCtrl;

    /// <summary>
    /// 角色受伤 委托
    /// </summary>
    public System.Action OnRoleHurt;

    /// <summary>
    /// 角色死亡 委托
    /// </summary>
    public System.Action<RoleCtrl> OnRoleDie;


    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();


        //CurRoleFSMMgr = new RoleFSMMgr(this);
      

        if (CurRoleType == RoleType.MainPlayer && CameraCtrl.Instance != null)
        {
            CameraCtrl.Instance.Init();
        }

        BornPos = transform.position;
    }


    private void Update()
    {
        if (m_CurRoleAI != null)
            m_CurRoleAI.DoAI();
        if (CurRoleFSMMgr != null)
            CurRoleFSMMgr.OnUpdate();



        if (CharacterController.isGrounded == false)
        {
            CharacterController.Move(transform.position + new Vector3(0, -100, 0) - transform.position);
        }


        if (CurRoleType == RoleType.MainPlayer &&  CameraCtrl.Instance != null)
        {
            CameraCtrl.Instance.transform.position = transform.position;
        }
    }

    private void OnDestroy()
    {
        if (m_RoleHeadBarCtrl != null)
        {
            Destroy(m_RoleHeadBarCtrl.gameObject);
        }
    }


    /// <summary>
    /// 初始化角色信息
    /// </summary>
    /// <param name="roleInfo"></param>
    /// <param name="roleAI"></param>
    public void Init(RoleType roleType,RoleInfo roleInfo, IRoleAI roleAI)
    {
        CurRoleType = roleType;
        CurRoleInfo = roleInfo;
        m_CurRoleAI = roleAI;

        CurRoleFSMMgr = new RoleFSMMgr(this);
        DoIdle();

        //加载头顶信息栏
        InitRoleHeadBar();
    }

    /// <summary>
    /// 初始化角色头顶信息
    /// </summary>
    private void InitRoleHeadBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;
        if (CurRoleInfo == null) return;
        if (m_HeadBarPos == null) return;

        GameObject roleHeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIOther, "RoleHeadBar", cache: true);
        roleHeadBar.transform.SetParent(RoleHeadBarRoot.Instance.transform);
        roleHeadBar.transform.localPosition = Vector3.zero;
        roleHeadBar.transform.localScale = Vector3.one;

        m_RoleHeadBarCtrl = roleHeadBar.GetComponent<RoleHeadBarCtrl>();

        //初始化
        m_RoleHeadBarCtrl.Init(m_HeadBarPos, CurRoleInfo.RoleNickName,CurRoleType!=RoleType.MainPlayer);
    }



    public void DoIdle()
    {
        CurRoleFSMMgr.ChangeState(RoleStateType.Idle);
    }
    
    /// <summary>
    /// 移动到目标点
    /// </summary>
    /// <param name="targetPos"></param>
    public void DoMove(Vector3 targetPos)
    {
        if (targetPos != Vector3.zero)
        {
            TargetPos = targetPos;
            TargetPos.y = 0;
            CurRoleFSMMgr.ChangeState(RoleStateType.Run);
        }
    }

    /// <summary>
    /// 收到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void DoHurt(int  damage,float delaytime)
    {
        StartCoroutine(IE_DoHurt(damage, delaytime));
    }

    IEnumerator IE_DoHurt(int damage, float delaytime)
    {
        yield return new WaitForSeconds(delaytime);

        int hurt = (int)(damage * Random.Range(0.5f, 1f));

        CurRoleInfo.CurHp -= hurt;

        //角色受伤委托
        if (OnRoleHurt != null) OnRoleHurt();


        if (m_RoleHeadBarCtrl != null)
        {
            m_RoleHeadBarCtrl.UpdHrut(hurt, (float)CurRoleInfo.CurHp/ CurRoleInfo.HpMax);
        }

        if (CurRoleInfo.CurHp > 0)
        {
            CurRoleFSMMgr.ChangeState(RoleStateType.Hurt);
        }
        else
        {
            CurRoleFSMMgr.ChangeState(RoleStateType.Die);
        }
    }
    
    public void DoDie()
    {
        CurRoleFSMMgr.ChangeState(RoleStateType.Die);
    }

    public void DoAttack()
    {
        if (LockEnemy == null) return;

        //切换攻击状态
        CurRoleFSMMgr.ChangeState(RoleStateType.Attack);

        //伤害目标
        //暂定
        LockEnemy.DoHurt(100, 0.5f);
    }
}
