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
        List<ProductEntity> datas = ProductDBModel.Instance.GetAll();
        for (int i = 0; i < datas.Count; i++)
        {
            Debug.Log(datas[i].Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
