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
        //short data = 100;
        //byte[] buffer =  BitConverter.GetBytes(data);
        //for (int i = 0; i < buffer.Length; i++)
        //{
        //    Debug.Log(string.Format("{0} = {1}",i,buffer[i]));
        //}
        //string data = "我是大笨蛋。";
        //using (MMO_MemoryStream ms = new MMO_MemoryStream())
        //{
        //    ms.WriteUTF8String(data);
        //    ms.Position = 0;

        //    Debug.Log(ms.ReadUTF8String());
        //}
        Debug.Log(ProductDBModel.Instance.Get(5).Name);
        ProductDBModel.Instance.GetAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
