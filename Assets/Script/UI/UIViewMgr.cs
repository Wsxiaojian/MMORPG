//***********************************************************
// 描述：UI窗口管理类
// 作者：fanwei 
// 创建时间：2021-03-27 06:45:40 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections.Generic;

/// <summary>
/// UI窗口管理类
/// </summary>
public class UIViewMgr : Singleton<UIViewMgr>
{
    private Dictionary<WindowUIType, ISystem> m_Dic;


    public UIViewMgr()
    {
        m_Dic = new Dictionary<WindowUIType, ISystem>();


        m_Dic.Add(WindowUIType.LogOn, AccountCtrl.Instance);
        m_Dic.Add(WindowUIType.Reg, AccountCtrl.Instance);
    }


    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="windowUIType"></param>
    public void OpenView(WindowUIType windowUIType)
    {
        if (m_Dic.ContainsKey(windowUIType))
        {
            m_Dic[windowUIType].OpenView(windowUIType);
        }
    }
}
