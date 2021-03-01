//***********************************************************
// 描述：这是一个功能性代码
// 作者：fanwei 
// 创建时间：2021-02-23 11:14:34
// 版本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInit : MonoBehaviour
{
    #region 常量
    /// <summary>
    /// 呢称
    /// </summary>
    public const string MMO_NICKNAME = "MMO_NICKNAME";
    /// <summary>
    /// 密码
    /// </summary>
    public const string MMO_PWD= "MMO_PWD";
    #endregion


    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1)});


    /// <summary>
    /// 当前玩家信息
    /// </summary>
    public string CurNickName;
    /// <summary>
    /// 当前玩家
    /// </summary>
    public RoleCtrl CurPlayer;


    public static GlobalInit  Instance;


    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }
}
