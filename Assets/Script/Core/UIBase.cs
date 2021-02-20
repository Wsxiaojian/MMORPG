//***********************************************************
// 描述：这是一个功能代码
// 作者：fanwei 
// 创建时间：2021-02-20 16:55:41
// 版 本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// UI 基类
/// </summary>
public class UIBase : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        Button[] buttons = transform.GetComponentsInChildren<Button>();
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                OnBtnClick(buttons[i]);
            }
        }

        OnStart();
    }


    protected virtual void OnAwake(){ }
    protected virtual void OnStart(){ }

    protected virtual void OnBtnClick(Button btn) { }
}
