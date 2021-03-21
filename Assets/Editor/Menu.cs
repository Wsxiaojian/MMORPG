//***********************************************************
// 描述：自定义工具菜单
// 作者：fanwei 
// 创建时间：2021-03-21 19:29:06 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 自定义工具菜单
/// </summary>
public class Menu 
{
    [MenuItem("GameTool/Seeting")]
    private static void OpenSettingWindow()
    {
        SettingWindow window = EditorWindow.GetWindow<SettingWindow>();
        window.titleContent = new GUIContent("全局设置");
        window.Show();

    }


}
