//***********************************************************
// 描述：
// 作者：fanwei 
// 创建日期：2021-02-23 07:34:00
// 版本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneLogOnCtrl : UISceneBase
{

    protected override void OnStart()
    {
        base.OnStart();

        //开启协程
        StartCoroutine(delayOpenLogOnWindow());
    }

    IEnumerator delayOpenLogOnWindow()
    {
        yield return new WaitForSeconds(0.5f);

        //打开登陆窗口
        UIViewUtil.Instance.OpenWindow(WindowUIType.LogOn);
    }
}
