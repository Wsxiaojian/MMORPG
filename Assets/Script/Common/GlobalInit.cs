//***********************************************************
// 描述：这是一个功能性代码
// 作者：fanwei 
// 创建时间：2021-02-23 11:14:34
// 版本：1.0
// 备注：
//***********************************************************
using System;
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

    /// <summary>
    /// web 账号服务器Url
    /// </summary>
    public const string WebAccountUrl = "http://192.168.81.200:8080/";
    #endregion


    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1)});


    private long m_ServerTime;
    /// <summary>
    /// 服务器时间
    /// </summary>
    public long ServerTime
    {
        get
        {
            return m_ServerTime;
        }
    }
    /// <summary>
    /// 服务器当前时间
    /// </summary>
    public long CurServerTime
    {
        get
        {
            return m_ServerTime + (long)Time.unscaledTime;
        }
    }

    /// <summary>
    /// 当前用户登陆实体
    /// </summary>
    public RetAccountEntity CurAccountEntity;

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

    private void Start()
    {
        NetWorkHttp.Instance.SendData(WebAccountUrl + "api/time",GetTimeCallBack);
    }

    /// <summary>
    /// 获取服务器时间回调
    /// </summary>
    /// <param name="obj"></param>
    private void GetTimeCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if(obj.HasError == false)
        {
            m_ServerTime = obj.Data.ToLong();
        }
    }
}
