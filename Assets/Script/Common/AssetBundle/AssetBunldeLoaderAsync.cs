//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-22 20:04:24 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBunldeLoaderAsync : MonoBehaviour
{
    private string m_FullFilePath;
    private string m_Name;
    private AssetBundleCreateRequest abRequest;
    private AssetBundle bundle;

    /// <summary>
    /// 加载结束回调
    /// </summary>
    public Action<UnityEngine.Object> OnComplete;

    public void Init(string path,string name)
    {
        m_FullFilePath = LocalFileMgr.Instance.LocalFilePath + path;
        m_Name = name;
    }

    private void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        abRequest = AssetBundle.LoadFromMemoryAsync(LocalFileMgr.Instance.GetByte(m_FullFilePath));
        yield return abRequest;
        bundle = abRequest.assetBundle;

        UnityEngine.Object obj = bundle.LoadAsset(m_Name);
        if (OnComplete != null)
        {
            OnComplete(obj);
        }
    }
}
