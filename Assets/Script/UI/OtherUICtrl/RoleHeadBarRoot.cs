//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-01 11:17:28 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleHeadBarRoot : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public static RoleHeadBarRoot Instance;

    private void Awake()
    {
        Instance = this;
    }

}
