//***********************************************************
// 描述：消息事件分发
// 作者：fanwei 
// 创建时间：2021-03-19 18:20:30 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections.Generic;

/// <summary>
/// 消息事件分发
/// </summary>
public class EventDispatch : Singleton<EventDispatch>
{
    /// <summary>
    /// 委托定义
    /// </summary>
    /// <param name="msgData">消息内容</param>
    public delegate void OnActionHandle(byte[] msgData);

    /// <summary>
    /// 事件注册 字典
    /// </summary>
    private Dictionary<ushort, List<OnActionHandle>> m_Event_Dic = new Dictionary<ushort, List<OnActionHandle>>();


    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="protaCode">协议号</param>
    /// <param name="onAction"></param>
    public void AddListenter(ushort protaCode, OnActionHandle onAction)
    {
        if (m_Event_Dic.ContainsKey(protaCode))
        {
            m_Event_Dic[protaCode].Add(onAction);
        }
        else
        {
            List<OnActionHandle> actionHandles = new List<OnActionHandle>();
            actionHandles.Add(onAction);
            m_Event_Dic[protaCode] = actionHandles;
        }
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="protaCode">协议号</param>
    /// <param name="onAction"></param>
    public void RemoveListenter(ushort protaCode, OnActionHandle onAction)
    {
        if (m_Event_Dic.ContainsKey(protaCode))
        {
            m_Event_Dic[protaCode].Remove(onAction);

            if(m_Event_Dic[protaCode].Count == 0)
            {
                m_Event_Dic.Remove(protaCode);
            }
        }
    }

    /// <summary>
    /// 分发消息
    /// </summary>
    /// <param name="protaCode">协议号</param>
    /// <param name="msgData">消息数据</param>
    public void Dispatch(ushort protaCode, byte[] msgData)
    {
        if (m_Event_Dic.ContainsKey(protaCode))
        {
            List<OnActionHandle> actionHandles = m_Event_Dic[protaCode];
            for (int i = 0; i < actionHandles.Count; i++)
            {
                actionHandles[i].Invoke(msgData);
            }
        }
    }
}
