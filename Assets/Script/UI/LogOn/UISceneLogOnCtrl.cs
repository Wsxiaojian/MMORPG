//***********************************************************
// ����������һ�����ܴ���
// ���ߣ�fanwei 
// ����ʱ�䣺2021-02-23 07:34:00
// �� ����1.0
// ��ע��
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneLogOnCtrl : UISceneBase
{


    protected override void OnStart()
    {
        base.OnStart();

        //打开登陆窗口
        WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }
}
