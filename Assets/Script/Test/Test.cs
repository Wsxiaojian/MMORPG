//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-11 18:43:31 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        //NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/account/3", GetCallBack);

        //if(NetWorkHttp.Instance.IsBusy == false)
        //{
        //    //pos参数
        //    LitJson.JsonData jsonData = new LitJson.JsonData();
        //    jsonData["Type"] = 0;  //0 注册 1登陆
        //    jsonData["UserName"] = "XXX";
        //    jsonData["Pwd"] = "123";

        //    NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/account", PostCallBack, true, jsonData.ToJson());
        //}

        //NetWorkSocket.Instance.Connect("192.168.81.131", 1011);

        //EventDispatch.Instance.AddListenter(ProtoCodeDef.Email_Request_All, RequestTest);

        //1.获取byte数组

        //AssetBundle.LoadFromMemory()

        //AssetBundleMgr.Instance.LoadClone(@"Role/role_mainplayer.assetbundle", "Role_MainPlayer");
        AssetBunldeLoaderAsync async=  AssetBundleMgr.Instance.LoadAsync(@"Role/role_mainplayer.assetbundle", "Role_MainPlayer");
        async.OnComplete = (obj) =>
         {
             Instantiate(obj);
         };
    }

    //private void RequestTest(byte[] msgData)
    //{
    //    EmailRequestAll email = EmailRequestAll.Get(msgData);

    //    Debug.LogFormat("客户端接收到消息:邮件编号：{0}邮件信息：{1}", email.EmailID, email.EmailInfo);
    //}

    //private void Send()
    //{
    //    EmailRequestAll email = new EmailRequestAll();
    //    email.EmailID = 1;
    //    email.EmailInfo = "发送给服务器测试";

    //    NetWorkSocket.Instance.SendMsg(email.ToArray());
    //}


    //private void GetCallBack(NetWorkHttp.CallBackArgs obj)
    //{
    //    if (obj.HasError)
    //    {
    //        Debug.Log(obj.ErrorMsg);
    //    }
    //    else
    //    {
    //        RetAccountEntity retAccountEntity = LitJson.JsonMapper.ToObject<RetAccountEntity>(obj.Json);

    //        Debug.Log(retAccountEntity.UserName);
    //    }
    //}


    //private void PostCallBack(NetWorkHttp.CallBackArgs obj)
    //{
    //    if (obj.HasError)
    //    {

    //    }
    //    else
    //    {
    //        RetValue ret = LitJson.JsonMapper.ToObject<RetValue>(obj.Json);


    //        Debug.Log(ret.RetData);
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Send();

            //SceneMgr.Instance.LoadMainCity();
        }
    }
}
