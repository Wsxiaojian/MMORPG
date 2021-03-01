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
    /// 动画机
    /// </summary>
    public Animator Animator;

    /// <summary>
    /// 角色状态机
    /// </summary>
    public RoleFSMMgr CurRoleFSMMgr;
    /// <summary>
    /// 角色Ai控制
    /// </summary>
    private IRoleAI m_CurRoleAI;
    /// <summary>
    /// 角色信息
    /// </summary>
    private RoleInfo m_CurRoleInfo;
    /// <summary>
    /// 角色类型
    /// </summary>
    [SerializeField]
    private RoleType m_RoleType = RoleType.None;


    /// <summary>
    /// 头顶点
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarPos;
    /// <summary>
    /// 头顶Bar显示控制
    /// </summary>
    private RoleHeadBarCtrl m_RoleHeadBarCtrl;


    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();


        CurRoleFSMMgr = new RoleFSMMgr(this);


        if (m_RoleType == RoleType.MainPlayer && CameraCtrl.Instance != null)
        {
            CameraCtrl.Instance.Init();
        }
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


        if (m_RoleType == RoleType.MainPlayer &&  CameraCtrl.Instance != null)
        {
            CameraCtrl.Instance.transform.position = transform.position;
        }
            
    }


    /// <summary>
    /// 初始化角色信息
    /// </summary>
    /// <param name="roleInfo"></param>
    /// <param name="roleAI"></param>
    public void Init(RoleType roleType,RoleInfo roleInfo, IRoleAI roleAI)
    {
        m_RoleType = roleType;
        m_CurRoleInfo = roleInfo;
        m_CurRoleAI = roleAI;

        CurRoleFSMMgr = new RoleFSMMgr(this);


        //加载头顶信息栏
        InitRoleHeadBar();
    }

    /// <summary>
    /// 初始化角色头顶信息
    /// </summary>
    private void InitRoleHeadBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;
        if (m_CurRoleInfo == null) return;
        if (m_HeadBarPos == null) return;

        GameObject roleHeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIOther, "RoleHeadBar", cache: true);
        roleHeadBar.transform.SetParent(RoleHeadBarRoot.Instance.transform);
        roleHeadBar.transform.localPosition = Vector3.zero;
        roleHeadBar.transform.localScale = Vector3.one;

        m_RoleHeadBarCtrl = roleHeadBar.GetComponent<RoleHeadBarCtrl>();

        //初始化
        m_RoleHeadBarCtrl.Init(m_HeadBarPos, m_CurRoleInfo.RoleNickName,m_RoleType!=RoleType.MainPlayer);
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
    public void DoHurt(int  damage)
    {
        if (m_RoleHeadBarCtrl != null)
        {
            m_RoleHeadBarCtrl.ShowHUD(damage, 0.5f);
        }
        CurRoleFSMMgr.ChangeState(RoleStateType.Hurt);
    }

    public void DoDie()
    {
        CurRoleFSMMgr.ChangeState(RoleStateType.Die);
    }

    public void DoAttack()
    {
        CurRoleFSMMgr.ChangeState(RoleStateType.Attack);
    }
}
