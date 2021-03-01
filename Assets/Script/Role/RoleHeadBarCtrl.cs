//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-01 10:22:36 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RoleHeadBarCtrl : MonoBehaviour
{
    /// <summary>
    /// 角色头顶目标点
    /// </summary>
    private Transform m_TargetPos;

    /// <summary>
    /// 角色名称
    /// </summary>
    private string m_NickName;


    /// <summary>
    /// 角色名称显示
    /// </summary>
    [SerializeField]
    private Text Txt_NickName;

    /// <summary>
    /// hp血条显示
    /// </summary>
    [SerializeField]
    private Transform Tf_HpSlider;
    /// <summary>
    /// 血条显示
    /// </summary>
    [SerializeField]
    private Image Img_HpSlider;

    /// <summary>
    /// hud信息 
    /// </summary>
    [SerializeField]
    private Transform Tf_HUDInfo;
    /// <summary>
    /// hud信息显示
    /// </summary>
    [SerializeField]
    private Text Txt_HUDPrefab;

    /// <summary>
    /// 位置偏移曲线
    /// </summary>
    [SerializeField]
    private AnimationCurve m_OffsetAnim;
    /// <summary>
    /// 缩放曲线
    /// </summary>
    [SerializeField]
    private AnimationCurve m_ScaleAnim;
    /// <summary>
    /// 透明度曲线
    /// </summary>
    [SerializeField]
    private AnimationCurve m_AlphaAnim;

    /// <summary>
    /// 当前产生的Hud显示
    /// </summary>
    private List<Text> CurHUD_List;

    /// <summary>
    /// 初始化 角色头目信息显示
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="nickName"></param>
    /// <param name="isShowHpBar">是否显示血条</param>
    public void Init(Transform targetPos, string nickName,bool isShowHpBar)
    {
        m_TargetPos = targetPos;
        m_NickName = nickName;

        //显示呢称
        if (m_TargetPos != null)
        {
            Txt_NickName.gameObject.SetActive(true);
            Txt_NickName.text = nickName;
        }
        else
        {
            Txt_NickName.gameObject.SetActive(false);
        }

        //显示血条
        if (isShowHpBar)
        {
            Tf_HpSlider.gameObject.SetActive(true);
            Img_HpSlider.fillAmount = 1;
        }
        else
        {
            Tf_HpSlider.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //头顶跟随
        if (m_TargetPos != null)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(m_TargetPos.position);

            transform.position = SceneUIMgr.Instance.CurUIScene.UICamera.ViewportToWorldPoint(viewPos);
        }
    }


    /// <summary>
    /// 显示HUD信息
    /// </summary>
    /// <param name="damage">伤害值</param>
    /// <param name="delayTime">延迟时间</param>
    public void ShowHUD(int  damage,int delayTime)
    {
        Text hudText = CurHUD_List.Find(text => text.gameObject.activeSelf == false);
        if(hudText == null)
        {
            hudText = Instantiate(Txt_HUDPrefab, Tf_HUDInfo);
        }

        hudText.text = string.Format("-{0}", damage);

        StartCoroutine(StartHUDAnim(hudText,delayTime));
    }

    /// <summary>
    /// HUd动画显示
    /// </summary>
    /// <param name="hudText"></param>
    /// <returns></returns>
    IEnumerator StartHUDAnim(Text hudText, int delayTime)
    {
        hudText.gameObject.SetActive(true);
        hudText.enabled = false;
        //等待延迟时间 显示
        yield return new WaitForSeconds(delayTime);

        //初始化信息
        hudText.enabled = true;
        hudText.transform.localPosition = Vector3.zero;
        hudText.transform.localScale = Vector3.zero;
        Color color = hudText.color;
        color.a = 0;
        hudText.color = color;

        //启动效果
        hudText.transform.DOLocalMoveY(1f, 1).SetEase(m_OffsetAnim);
        hudText.transform.DOScale(Vector3.one, 1).SetEase(m_ScaleAnim);
        hudText.DOFade(1, 1).SetEase(m_AlphaAnim);

       yield return new WaitForSeconds(1);
        hudText.gameObject.SetActive(false);
    }
}
