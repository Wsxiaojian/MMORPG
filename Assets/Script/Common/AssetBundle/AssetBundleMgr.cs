//***********************************************************
// 描述：AssetBundle 资源加载管理
// 作者：fanwei 
// 创建时间：2021-03-22 16:06:56 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEngine;

/// <summary>
/// AssetBundle 资源加载管理
/// </summary>
public class AssetBundleMgr : Singleton<AssetBundleMgr>
{
    /// <summary>
    /// 同步加载 资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject Load(string path ,string name)
    {
        using (AssetBundleLoader assetBundleLoader = new AssetBundleLoader(path))
        {
            return assetBundleLoader.Load<GameObject>(name);
        }
    }

    /// <summary>
    /// 同步加载 镜像
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadClone(string path, string name)
    {
        using (AssetBundleLoader assetBundleLoader = new AssetBundleLoader(path))
        {
            GameObject obj = assetBundleLoader.Load<GameObject>(name);
            return Object.Instantiate(obj);
        }
    }

    /// <summary>
    /// 异步加载 资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public AssetBunldeLoaderAsync LoadAsync(string path, string name)
    {
        GameObject obj = new GameObject("LoadAsync");
        AssetBunldeLoaderAsync async = obj.GetOrCreateComponent<AssetBunldeLoaderAsync>();
        async.Init(path, name);
        return async;
    }
}
