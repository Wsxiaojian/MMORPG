//***********************************************************
// 描述：
// 作者：fanwei 
// 创建日期：2021-02-23 07:37:39
// 版本：1.0
// 备注：
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
