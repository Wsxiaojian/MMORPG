//***********************************************************
// 描述：初始化场景控制
// 作者：fanwei 
// 创建时间：2021-02-23 13:46:42
// 版本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化场景控制
/// </summary>
public class InitSceneCtrl : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(LoadLogOn());
    }

    private IEnumerator LoadLogOn()
    {
        yield return new WaitForSeconds(2f);

        SceneMgr.Instance.LoadLogOn();
    }
}
