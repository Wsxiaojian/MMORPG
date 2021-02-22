//***********************************************************
// 描述：资源加载管理
// 作者：fanwei 
// 创建时间：2021-02-20 18:21:38
// 版 本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ResourcesMgr : Singleton<ResourcesMgr>
{
    #region 资源类型 ResourceType
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// 场景UI
        /// </summary>
        UIScene,
        /// <summary>
        /// 窗口UI
        /// </summary>
        UIWindow,
        /// <summary>
        /// 角色
        /// </summary>
        Role,
        /// <summary>
        /// 特效
        /// </summary>
        Effect,
    }
    #endregion

    //资源缓存
    private Hashtable m_DicRes = new Hashtable();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resourceType"></param>
    /// <param name="path"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public GameObject Load(ResourceType resourceType, string path, bool cache = false)
    {
        Object objPrefab;
        if (m_DicRes.ContainsKey(path))
        {
            objPrefab = (Object)m_DicRes[path];
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            switch (resourceType)
            {
                case ResourceType.UIScene:
                    sb.Append("UIPrefab/SceneUI/");
                    break;
                case ResourceType.UIWindow:
                    sb.Append("UIPrefab/WindowUI/");
                    break;
                case ResourceType.Role:
                    sb.Append("RolePrefab/");
                    break;
                case ResourceType.Effect:
                    sb.Append("EfectPrefab/");
                    break;
            }
            sb.Append(path);

            objPrefab = Resources.Load(sb.ToString());

            //缓存
            if (cache)
            {
                m_DicRes.Add(path, objPrefab);
            }
        }

        //实例化
        GameObject obj = GameObject.Instantiate(objPrefab) as GameObject;

        return obj;
    }

    /// <summary>
    /// 销毁资源
    /// </summary>
    public override void Dispose()
    {
        m_DicRes.Clear();
        Resources.UnloadUnusedAssets();
    }
}
