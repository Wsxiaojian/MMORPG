//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-02-26 06:38:48 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoleStateAbstract
{
    /// <summary>
    /// 角色管理器
    /// </summary>
    protected RoleFSMMgr RoleFSMMgr;

    protected AnimatorStateInfo AnimStateInfo;

    public RoleStateAbstract(RoleFSMMgr roleFsm)
    {
        RoleFSMMgr = roleFsm;
    }

    /// <summary>
    /// 进入状态 
    /// </summary>
    public virtual void OnEnter() { }
    /// <summary>
    /// 更新状态
    /// </summary>
    public virtual void OnUpdate() { }
    /// <summary>
    /// 退出状态
    /// </summary>
    public virtual void OnExit() { }

}
