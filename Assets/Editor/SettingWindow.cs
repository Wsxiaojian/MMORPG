//***********************************************************
// 描述：全局设置窗口
// 作者：fanwei 
// 创建时间：2021-03-21 19:32:37 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 全局设置窗口
/// </summary>
public class SettingWindow : EditorWindow
{

    private List<MacroItem> m_MacroItems = new List<MacroItem>();
    private Dictionary<string, bool> m_MacroVisable_Dic = new Dictionary<string, bool>();
    private string m_MacroStr;


    private void OnEnable()
    {
        m_MacroStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);

        m_MacroItems.Clear();
        m_MacroItems.Add(new MacroItem() { Name = "DEBUG_MODEL", DisplayName = "调试模式", IsDebug = true, IsRelease = false });
        m_MacroItems.Add(new MacroItem() { Name = "DEBUG_LOG", DisplayName = "打印日志", IsDebug = true, IsRelease = false });
        m_MacroItems.Add(new MacroItem() { Name = "STAT_TD", DisplayName = "开启统计", IsDebug = false, IsRelease = true });


        m_MacroVisable_Dic.Clear();
        for (int i = 0; i <m_MacroItems.Count; i++)
        {
            if(!string.IsNullOrEmpty(m_MacroStr) && m_MacroStr.IndexOf(m_MacroItems[i].Name) != -1)
            {
                m_MacroVisable_Dic.Add(m_MacroItems[i].Name, true);
            }
            else
            {
                m_MacroVisable_Dic.Add(m_MacroItems[i].Name, false);
            }
        }
    }


    private void OnGUI()
    {
        //显示列表
        for (int i = 0; i < m_MacroItems.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("Box");
            m_MacroVisable_Dic[m_MacroItems[i].Name] = EditorGUILayout.ToggleLeft(m_MacroItems[i].DisplayName, m_MacroVisable_Dic[m_MacroItems[i].Name]);
            EditorGUILayout.EndHorizontal();
        }

        //按钮
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("保存"))
        {
            //写入
            SaveMacro();
        }
        if (GUILayout.Button("调试模式"))
        {
            for (int i = 0; i < m_MacroItems.Count; i++)
            {
                m_MacroVisable_Dic[m_MacroItems[i].Name] = m_MacroItems[i].IsDebug;
            }
            SaveMacro();
        }
        if (GUILayout.Button("发布模式"))
        {
            for (int i = 0; i < m_MacroItems.Count; i++)
            {
                m_MacroVisable_Dic[m_MacroItems[i].Name]= m_MacroItems[i].IsRelease;
            }
            SaveMacro();
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 保存宏定义
    /// </summary>
    private void SaveMacro()
    {
        m_MacroStr = string.Empty;
        foreach (var item in m_MacroVisable_Dic)
        {
            if (item.Value)
            {
                m_MacroStr+=string.Format("{0};",item.Key);
            }
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_MacroStr);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_MacroStr);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_MacroStr);
    }

    /// <summary>
    /// 宏项目
    /// </summary>
    public class MacroItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 显示名称 中文
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// 是否是调试项
        /// </summary>
        public bool IsDebug;
        /// <summary>
        /// 是否是发布项
        /// </summary>
        public bool IsRelease;
    }
}
