//***********************************************************
// 描述：loadging进度显示控制
// 作者：fanwei 
// 创建时间：2021-02-23 14:20:20
// 版本：1.0
// 备注：
//***********************************************************
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// loadging进度显示控制
/// </summary>
public class UISceneLoadingCtrl : UISceneBase
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
        m_Txt_Process.text = string.Format("{0}%", (int)(value*100));

        _Tf_ProcessAnim.localPosition = new Vector3(760 * value, 0, 0);
    }

     
}
