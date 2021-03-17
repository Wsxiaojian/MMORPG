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

        if(NetWorkHttp.Instance.IsBusy == false)
        {
            //pos参数
            LitJson.JsonData jsonData = new LitJson.JsonData();
            jsonData["Type"] = 0;  //0 注册 1登陆
            jsonData["UserName"] = "XXX";
            jsonData["Pwd"] = "123";

            NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/account", PostCallBack, true, jsonData.ToJson());
        }

    }



    private void GetCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            Debug.Log(obj.ErrorMsg);
        }
        else
        {
            RetAccountEntity retAccountEntity = LitJson.JsonMapper.ToObject<RetAccountEntity>(obj.Json);

            Debug.Log(retAccountEntity.UserName);
        }
    }


    private void PostCallBack(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {

        }
        else
        {
            RetValue ret = LitJson.JsonMapper.ToObject<RetValue>(obj.Json);


            Debug.Log(ret.RetData);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
