//***********************************************************
// 描述：所有UI基类
// 作者：fanwei 
// 创建时间：2021-03-26 09:00:10 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 所有UI基类
/// </summary>
public class UIViewBase : MonoBehaviour
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
            foreach (Button btn in buttons)
            {
                EventTriggerListener.Get(btn.gameObject).onClick = OnBtnClick;
            }
        }

        OnStart();
    }


    private void OnDestroy()
    {
        BeforeDestroy();
    }

    /// <summary>
    /// 子类重写生命周期
    /// </summary>
    protected virtual void OnAwake() { }
    /// <summary>
    /// 子类重写生命周期
    /// </summary>
    protected virtual void OnStart() { }

    /// <summary>
    /// 销毁前回调
    /// </summary>
    protected virtual void BeforeDestroy() { }

    /// <summary>
    /// 按钮点击事件
    /// </summary>
    /// <param name="btn"></param>
    protected virtual void OnBtnClick(GameObject btnGo) { }
}
