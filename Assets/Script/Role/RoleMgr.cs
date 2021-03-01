//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-01 10:04:00 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMgr : Singleton<RoleMgr>
{

    /// <summary>
    /// 加载角色对象 LoadRole
    /// </summary>
    /// <param name="roleType">角色类型</param>
    /// <param name="name">角色名称</param>
    /// <returns></returns>
    public GameObject LoadRole(RoleType roleType,string name)
    {
        string path = string.Empty;
        switch (roleType)
        {
            case RoleType.MainPlayer:
                path = "Player";
                break;
            case RoleType.Monster:
                path = "Monster";
                break;
        }

        GameObject roleGo = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Role,string.Format("{0}/{1}",path,name),cache:true);

        return roleGo;
    }

    public override void Dispose()
    {
        base.Dispose();

    }
}
