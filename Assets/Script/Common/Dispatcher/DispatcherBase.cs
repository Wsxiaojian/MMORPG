//***********************************************************
// 描述：事件分发器基类
// 作者：fanwei 
// 创建时间：2021-03-26 08:44:46 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections.Generic;

/// <summary>
/// 事件分发器基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="K">事件key</typeparam>
/// <typeparam name="P">事件参数类型</typeparam>
public class DispatcherBase<T, K, P> : IDisposable
    where T : class, new()
    where P : class
{
    #region  单例
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    #endregion

    #region 参数

    /// <summary>
    /// 委托定义
    /// </summary>
    /// <param name="msgData">消息内容</param>
    public delegate void OnActionHandle(P parms);

    /// <summary>
    /// 事件注册 字典
    /// </summary>
    private Dictionary<K, List<OnActionHandle>> m_Event_Dic = new Dictionary<K, List<OnActionHandle>>();
    #endregion

    #region AddListenter 添加事件监听
    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="protaCode">协议号</param>
    /// <param name="onAction"></param>
    public void AddListenter(K key, OnActionHandle onAction)
    {
        if (m_Event_Dic.ContainsKey(key))
        {
            m_Event_Dic[key].Add(onAction);
        }
        else
        {
            List<OnActionHandle> actionHandles = new List<OnActionHandle>();
            actionHandles.Add(onAction);
            m_Event_Dic[key] = actionHandles;
        }
    }
    #endregion

    #region  RemoveListenter 移除事件监听
    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="protaCode">协议号</param>
    /// <param name="onAction"></param>
    public void RemoveListenter(K key, OnActionHandle onAction)
    {
        if (m_Event_Dic.ContainsKey(key))
        {
            m_Event_Dic[key].Remove(onAction);

            if (m_Event_Dic[key].Count == 0)
            {
                m_Event_Dic.Remove(key);
            }
        }
    }
    #endregion

    #region Dispatch 分发消息
    /// <summary>
    /// 分发消息 无参数
    /// </summary>
    /// <param name="key"></param>
    public void Dispatch(K key)
    {
        Dispatch(key, null);
    }

    /// <summary>
    /// 分发消息
    /// </summary>
    /// <param name="protaCode">协议号</param>
    /// <param name="msgData">消息数据</param>
    public void Dispatch(K key, P parms)
    {
        if (m_Event_Dic.ContainsKey(key))
        {
            List<OnActionHandle> actionHandles = m_Event_Dic[key];
            for (int i = 0; i < actionHandles.Count; i++)
            {
                actionHandles[i].Invoke(parms);
            }
        }
    }
    #endregion

    #region 销毁
    /// <summary>
    /// 销毁
    /// </summary>
    public virtual void Dispose()
    {
        m_Event_Dic.Clear();
        m_Event_Dic = null;
    }
    #endregion
}
