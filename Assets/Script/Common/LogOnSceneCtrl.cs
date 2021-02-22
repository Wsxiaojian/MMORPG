//***********************************************************
// ����������һ�����ܴ���
// ���ߣ�fanwei 
// ����ʱ�䣺2021-02-23 07:37:39
// �� ����1.0
// ��ע��
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOnSceneCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIType.LogOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
