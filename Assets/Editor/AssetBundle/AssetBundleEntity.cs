//***********************************************************
// 描述：单个AssetBundle实体
// 作者：fanwei 
// 创建时间：2021-03-22 07:10:00 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections.Generic;

/// <summary>
/// 单个AssetBundle实体
/// </summary>
public class AssetBundleEntity
{
    /// <summary>
    /// 字典标识
    /// </summary>
    public string Key;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 标记
    /// </summary>
    public string Tag;

    /// <summary>
    /// 版本
    /// </summary>
    public int Version;

    /// <summary>
    /// 文件大小
    /// </summary>
    public long Size;

    /// <summary>
    /// 保存路径
    /// </summary>
    public string ToPath;

    /// <summary>
    /// 资源路径
    /// </summary>
    private List<string> pathList;
    /// <summary>
    /// 资源路径
    /// </summary>
    public List<string> PathList
    {
        get {
            if (pathList == null)
                pathList = new List<string>();
            return pathList;
        }
    }
}
