//***********************************************************
// 描述：GameObject扩展类
// 作者：fanwei 
// 创建时间：2021-03-17 15:17:48 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEngine;

/// <summary>
/// GameObject扩展类
/// </summary>
public static  class GameObjectUtils
{

    /// <summary>
    /// 获取或创建一个组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static T GetOrCreateComponent<T>(this GameObject gameObject) where T : Component
    {
        T ret;
        ret = gameObject.GetComponent<T>();
        if(ret == null)
        {
            ret = gameObject.AddComponent<T>();
        }
        return ret; 
    }
}
