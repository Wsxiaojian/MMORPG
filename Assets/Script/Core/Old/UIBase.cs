//***********************************************************
// 描述：UI 基类
// 作者：fanwei 
// 创建时间：2021-02-20 16:55:41
// 版 本：1.0
// 备注：
//***********************************************************
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
                Button btn = buttons[i];
                btn.onClick.AddListener(() => OnBtnClick(btn));
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
    protected virtual void OnAwake(){ }
    /// <summary>
    /// 子类重写生命周期
    /// </summary>
    protected virtual void OnStart(){ }

    /// <summary>
    /// 销毁前回调
    /// </summary>
    protected virtual void BeforeDestroy(){ }

    /// <summary>
    /// 按钮点击事件
    /// </summary>
    /// <param name="btn"></param>
    protected virtual void OnBtnClick(Button btn) { }
}
