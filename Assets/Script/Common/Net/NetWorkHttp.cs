//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-17 15:42:53 
// 版本：1.0 
// 备注：
//***********************************************************
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetWorkHttp : SingletonMono<NetWorkHttp>
{
    #region 属性
    /// <summary>
    /// 请求数据回调
    /// </summary>
    private Action<CallBackArgs> m_CallBack;
    /// <summary>
    /// 回调参数
    /// </summary>
    private CallBackArgs m_CallBackArgs;

    /// <summary>
    /// 是否繁忙
    /// </summary>
    private bool m_IsBusy;
    /// <summary>
    /// 是否繁忙
    /// </summary>
    public bool IsBusy
    {
        get
        {
            return m_IsBusy;
        }
    }
    #endregion

    protected override void OnStart()
    {
        base.OnStart();
        m_CallBackArgs = new CallBackArgs();
    }


    #region 向服务器发送消息
    /// <summary>
    /// 向服务器发送消息
    /// </summary>
    /// <param name="url">Url地址</param>
    /// <param name="callBack">回调</param>
    /// <param name="isPost">是否是pos请求</param>
    /// <param name="json">post请求表单参数</param>
    public void SendData(string url, Action<CallBackArgs> callBack, bool isPost = false, Dictionary<string,object> dic =null)
    {
        if (m_IsBusy) return;

        m_IsBusy = true;
        m_CallBack = callBack;
        if (isPost == false)
        {
            GetUrl(url);
        }
        else
        {
            if(dic == null || dic.Count == 0)
            {
                PostUrl(url, null);
            }
            else
            {
                long time = GlobalInit.Instance.CurServerTime;

                dic["deviceUniqueIdentifier"] = DeviceUtil.DeviceUniqueIdentifier;
                dic["deviceModel"] = DeviceUtil.DeviceModel;
                dic["sign"] = EncryptUtil.Md5(string.Format("{0}:{1}", time, DeviceUtil.DeviceUniqueIdentifier));
                dic["t"] = time;

                PostUrl(url, JsonMapper.ToJson(dic));
            }
        }
    }
    #endregion

    #region Get 方式  URl请求
    /// <summary>
    /// Get 方式  URl请求
    /// </summary>
    /// <param name="url"></param>
    private void GetUrl(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        StartCoroutine(Request(webRequest));
    }
    #endregion

    #region Post 方式  URl请求
    /// <summary>
    ///  Post 方式  URl请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    private void PostUrl(string url,string json)
    {
        //表单
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("", json);

        UnityWebRequest webRequest = UnityWebRequest.Post(url, wwwForm);
        StartCoroutine(Request(webRequest));
    }
    #endregion

    #region web请求等待协程
    /// <summary>
    /// web请求等待协程
    /// </summary>
    /// <param name="webRequest"></param>
    /// <returns></returns>
    private IEnumerator Request(UnityWebRequest webRequest)
    {
        yield return webRequest.SendWebRequest();

        m_IsBusy = false;

        if (webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            //错误
            m_CallBackArgs.HasError = true;
            m_CallBackArgs.ErrorMsg = webRequest.error;
        }
        else
        {
            if (webRequest.downloadHandler.text == "null")
            {
                m_CallBackArgs.HasError = true;
                m_CallBackArgs.ErrorMsg = "找不到用户！";
            }
            else
            {
                //正确
                //RetValue ret = JsonMapper.ToObject<RetValue>(webRequest.downloadHandler.text);

                //m_CallBackArgs.HasError = ret.HasError;
                //m_CallBackArgs.ErrorMsg = ret.ErrorMsg;
                //m_CallBackArgs.Json = ret.Value;

                m_CallBackArgs.HasError = false;
                m_CallBackArgs.Data = webRequest.downloadHandler.text;
            }
        }
        m_CallBack?.Invoke(m_CallBackArgs);
    }
    #endregion

    #region 网络回调参数
    /// <summary>
    /// 网络回调参数
    /// </summary>
    public class CallBackArgs : EventArgs
    {
        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool HasError;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg;
        /// <summary>
        /// 数据
        /// </summary>
        public string Data;
    }
    #endregion
}
