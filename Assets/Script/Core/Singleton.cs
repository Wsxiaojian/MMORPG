//***********************************************************
// 描述：单例
// 作者：fanwei 
// 创建时间：2021-02-20 17:02:02
// 版 本：1.0
// 备注：
//***********************************************************
using System;

/// <summary>
/// 单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : IDisposable where T : new()
{

    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }


    /// <summary>
    /// 消除接口
    /// </summary>
    public virtual void Dispose() { }
}
