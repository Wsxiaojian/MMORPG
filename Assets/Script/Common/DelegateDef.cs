//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-21 17:44:28 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateDef : Singleton<DelegateDef>
{

    /// <summary>
    /// 场景加载完成
    /// </summary>
    public Action OnSceneLoadOk;

}
