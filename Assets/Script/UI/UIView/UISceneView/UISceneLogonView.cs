//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-26 09:15:12 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录场景显示
/// </summary>
public class UISceneLogonView : UISceneViewBase
{
    protected override void OnStart()
    {
        base.OnStart();

        //开启协程
        StartCoroutine(delayOpenLogOnWindow());
    }

    IEnumerator delayOpenLogOnWindow()
    {
        yield return new WaitForSeconds(2);

        //打开登陆窗口
        UIViewMgr.Instance.OpenView(WindowUIType.LogOn);
    }
}
