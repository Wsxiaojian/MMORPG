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
    public CharacterController CharacterController;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed;
    /// <summary>
    /// 目标点
    /// </summary>
    public Vector3 TargetPos;


    /// <summary>
    /// 动画机
    /// </summary>
    public Animator Animator;

    /// <summary>
    /// 角色状态机
    /// </summary>
    private RoleFSMMgr m_RoleFSMMgr;
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

        MoveSpeed = 5f;

        m_RoleFSMMgr = new RoleFSMMgr(this);

    }


    private void Update()
    {
        if (m_CurRoleAI != null)
            m_CurRoleAI.DoAI();
        if (m_RoleFSMMgr != null)
            m_RoleFSMMgr.OnUpdate();



        if (CharacterController.isGrounded == false)
        {
            CharacterController.Move(transform.position + new Vector3(0, -100, 0) - transform.position);
        }
        

        if (CameraCtrl.Instance == null) return;
        CameraCtrl.Instance.transform.position = transform.position;
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

        m_RoleFSMMgr = new RoleFSMMgr(this);


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
        m_RoleFSMMgr.ChangeState(RoleStateType.Idle);
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
            m_RoleFSMMgr.ChangeState(RoleStateType.Run);
        }
    }

    public void DoHurt()
    {
        m_RoleFSMMgr.ChangeState(RoleStateType.Hurt);
    }

    public void DoDie()
    {
        m_RoleFSMMgr.ChangeState(RoleStateType.Die);
    }

    public void DoAttack()
    {
        m_RoleFSMMgr.ChangeState(RoleStateType.Attack);
    }
}
