//***********************************************************
// 描述：异步AssetBundle加载器
// 作者：fanwei 
// 创建时间：2021-03-22 20:04:24 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// 异步AssetBundle加载器
/// </summary>
public class AssetBunldeLoaderAsync : MonoBehaviour
{
    /// <summary>
    /// assetbunle文件全路径
    /// </summary>
    private string m_FullFilePath;
    /// <summary>
    /// 加载资源名称
    /// </summary>
    private string m_Name;
    /// <summary>
    /// 异步Assetbundle请求
    /// </summary>
    private AssetBundleCreateRequest abRequest;
    /// <summary>
    /// Assetbundle
    /// </summary>
    private AssetBundle bundle;

    /// <summary>
    /// 加载结束回调
    /// </summary>
    public Action<UnityEngine.Object> OnComplete;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    public void Init(string path, string name)
    {
        m_FullFilePath = LocalFileMgr.Instance.LocalFilePath + path;
        m_Name = name;
    }

    private void Start()
    {
        StartCoroutine(Load());
    }

    /// <summary>
    /// 协程加载
    /// </summary>
    /// <returns></returns>
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
