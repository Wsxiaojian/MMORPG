//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-06-01 09:40:56 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPath
{
    /// <summary>
    /// 打包AssetBundle路径
    /// </summary>
    public string BuildAssetBundlePath
    {
        get
        {
            return Application.dataPath + "/../MyAssetBundles";
        }
    }


}
