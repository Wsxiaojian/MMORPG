//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-26 09:02:30 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowViewBase : UIViewBase
{
    /// <summary>
    /// 窗口挂载点
    /// </summary>
    public WindowUIContainerType WindowUIContainerType;

    /// <summary>
    /// 窗口动画类型
    /// </summary>
    public WindowShowStyle WindowShowStyle;

    /// <summary>
    /// 动画持续时间
    /// </summary>
    public float Duration;

    /// <summary>
    /// 当前UI 的类型
    /// </summary>
    [HideInInspector]
    public WindowUIType CurrentUIType;


    /// <summary>
    /// 下一个打开窗口类型
    /// </summary>
    public WindowUIType NextUIType;


    /// <summary>
    ///  关闭窗口
    /// </summary>
    public virtual void Close()
    {
        UIViewUtil.Instance.CloseWindow(CurrentUIType);
    }

    public virtual void Close(WindowUIType nextUIType)
    {
        NextUIType = nextUIType;
        UIViewUtil.Instance.CloseWindow(CurrentUIType);
    }

    /// <summary>
    /// 销毁前  跳转下一个窗口
    /// </summary>
    protected override void BeforeDestroy()
    {
        LayerUIMgr.Instance.CheckOpenWindow();

        if(NextUIType!= WindowUIType.None)
        {
            UIViewMgr.Instance.OpenView(NextUIType);
        }
    }

    //按钮点击
    protected override void OnBtnClick(GameObject btnGo)
    {
        base.OnBtnClick(btnGo);

        //关闭按钮
        if(btnGo.name.Equals("Btn_Close", System.StringComparison.CurrentCultureIgnoreCase))
        {
            //关闭窗口
            Close();
        }
    }
}
