//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-02 18:11:21 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 玩家右上角 角色信息显示
/// </summary>
public class RoleInfoCtrl : MonoBehaviour
{
    public static RoleInfoCtrl Instance;

    /// <summary>
    /// 角色姓名
    /// </summary>
    [SerializeField]
    private Text Txt_RoleName;
    /// <summary>
    /// 角色血条
    /// </summary>
    [SerializeField]
    private Image Img_Hp;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (GlobalInit.Instance.CurPlayer != null)
        {
            GlobalInit.Instance.CurPlayer.OnRoleHurt = OnHurt;
        }
    }

    /// <summary>
    /// 初始化信息显示
    /// </summary>
    /// <param name="roleInfoMainPlayer"></param>
    public void SetInfo(RoleInfoMainPlayer roleInfoMainPlayer)
    {
        Txt_RoleName.text = roleInfoMainPlayer.RoleNickName;

        Img_Hp.fillAmount = (float)roleInfoMainPlayer.CurHp/ roleInfoMainPlayer.HpMax;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnHurt()
    {
        Img_Hp.DOFillAmount((float)GlobalInit.Instance.CurPlayer.CurRoleInfo.CurHp / GlobalInit.Instance.CurPlayer.CurRoleInfo.HpMax,0.5f);
    }
}
