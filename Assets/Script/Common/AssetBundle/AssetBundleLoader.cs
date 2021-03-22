//***********************************************************
// 描述：AssetBundle同步加载器
// 作者：fanwei 
// 创建时间：2021-03-22 19:16:37 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEngine;

/// <summary>
/// AssetBundle同步加载器
/// </summary>
public class AssetBundleLoader : System.IDisposable
{
    private AssetBundle bundle = null;

    public AssetBundleLoader(string path)
    {
        string fullFilePath = LocalFileMgr.Instance.LocalFilePath + path;

        bundle = AssetBundle.LoadFromMemory(LocalFileMgr.Instance.GetByte(fullFilePath));
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T Load<T>(string name) where T : Object
    {
        if (bundle == null) return default(T);
        return bundle.LoadAsset<T>(name);
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Dispose()
    {
        if (bundle != null) bundle.Unload(false);
    }
}
