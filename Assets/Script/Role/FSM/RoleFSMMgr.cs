//***********************************************************
// 描述：角色状态机
// 作者：fanwei 
// 创建时间：2021-02-26 06:39:37 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色状态机
/// </summary>
public class RoleFSMMgr
{
    public RoleCtrl RoleCtrl { get; private set; }

    /// <summary>
    /// 当前状态类型
    /// </summary>
    public RoleStateType CurRoleStateType
    {
        get;
        private set;
    }

    /// <summary>
    /// 当前角色状态
    /// </summary>
    public RoleStateAbstract CurRoleState
    {
        get;
        private set;
    }


    /// <summary>
    /// 角色状态
    /// </summary>
    private Dictionary<RoleStateType, RoleStateAbstract> m_DicRoleStates;



    public RoleFSMMgr(RoleCtrl roleCtrl)
    {
        RoleCtrl = roleCtrl;

        m_DicRoleStates = new Dictionary<RoleStateType, RoleStateAbstract>();

        m_DicRoleStates.Add(RoleStateType.Idle, new RoleStateIdle(this));
        m_DicRoleStates.Add(RoleStateType.Run, new RoleStateRun(this));
        m_DicRoleStates.Add(RoleStateType.Attack, new RoleStateAttack(this));
        m_DicRoleStates.Add(RoleStateType.Hurt, new RoleStateHurt(this));
        m_DicRoleStates.Add(RoleStateType.Die, new RoleStateDie(this));
    }


    /// <summary>
    /// 改变当前状态
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(RoleStateType newState)
    {
        if (CurRoleStateType == newState) return;


        //退出之前状态
        if (CurRoleState != null)
            CurRoleState.OnExit();

        //设置新状态
        CurRoleStateType = newState;
        CurRoleState = m_DicRoleStates[newState];

        //进入新状态
        CurRoleState.OnEnter();
    }

}
