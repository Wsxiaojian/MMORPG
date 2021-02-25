//***********************************************************
// 描述：角色信息
// 作者：fanwei 
// 创建时间：2021-02-26 06:40:30 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色信息
/// </summary>
public class RoleInfo
{
    /// <summary>
    /// 角色服务器id
    /// </summary>
    public int RoleServerID;
    /// <summary>
    /// 角色id
    /// </summary>
    public int RoleID;
    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleNickName;
    /// <summary>
    /// 角色最大生命值
    /// </summary>
    public int HpMax;
    /// <summary>
    /// 角色当前生命值
    /// </summary>
    public int CurHp;
}