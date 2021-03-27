//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-27 06:19:14 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// loading场景显示
/// </summary>
public class UISceneLoadingView : UISceneLogonView
{
    /// <summary>
    /// 进度条
    /// </summary>
    [SerializeField]
    private Slider m_LoadingSlider;

    /// <summary>
    /// 进度值
    /// </summary>
    [SerializeField]
    private Text m_Txt_Process;

    /// <summary>
    /// 帧动画
    /// </summary>
    [SerializeField]
    private Transform _Tf_ProcessAnim;

    protected override void OnStart()
    {
        base.OnStart();
        SetProcess(0f);
    }

    /// <summary>
    /// 设置loading进度值
    /// </summary>
    /// <param name="value"></param>
    public void SetProcess(float value)
    {
        m_LoadingSlider.value = value;
        m_Txt_Process.text = string.Format("{0}%", (int)(value * 100));

        _Tf_ProcessAnim.localPosition = new Vector3(760 * value, 0, 0);
    }
}
