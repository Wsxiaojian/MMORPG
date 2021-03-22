//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-22 07:11:56 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleWindow : EditorWindow
{
    private List<AssetBundleEntity> m_EntityList = null;
    private Dictionary<string,bool> m_Dic = null;


    private string[] tagArrs = new string[] { "All", "Role", "Scene", "Effect", "Audio", "None" };
    private int tagIndex = 0;


    private string[] targetArrs = new string[] { "Window", "Android", "IOS" };
#if UNITY_STANDALONE_WIN
    private int targetIndex = 0;
    private BuildTarget target = BuildTarget.StandaloneWindows;
#elif UNITY_ANDROID
    private int targetIndex = 1;
    private BuildTarget target = BuildTarget.Android;
#elif UNITY_IPHONE
    private int targetIndex = 2;
      private BuildTarget target = BuildTarget.iOS;
#endif

    public AssetBundleWindow()
    {
        AssetBundleDAL assetBundleDAL = new AssetBundleDAL(@"I:\workSpace\MyGitSpace\MMORPG\Assets\Editor\AssetBundle\AssetBundleConfig.xml");
        m_EntityList = assetBundleDAL.Get();

        m_Dic = new Dictionary<string, bool>();
        for (int i = 0; i < m_EntityList.Count; i++)
        {
            m_Dic[m_EntityList[i].Key] = true;
        }
    }


    private void OnGUI()
    {
        //按钮
        EditorGUILayout.BeginHorizontal("box");
        tagIndex = EditorGUILayout.Popup(tagIndex, tagArrs, GUILayout.Width(70));
        if (GUILayout.Button("选定Tag",GUILayout.Width(100)))
        {
            ChooseTagHandler();
        }
        targetIndex = EditorGUILayout.Popup(targetIndex, targetArrs, GUILayout.Width(80));
        if (GUILayout.Button("选定平台", GUILayout.Width(100)))
        {
            ChooseTargetHandler();
        }
        if (GUILayout.Button("打包", GUILayout.Width(100)))
        {
            BuildHandler();
        }
        if (GUILayout.Button("清空目录", GUILayout.Width(100)))
        {
            ClearAssetHandler();
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal("box");
        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("保存路径", GUILayout.Width(100));
        GUILayout.Label("版本", GUILayout.Width(100));
        GUILayout.Label("大小", GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < m_EntityList.Count; i++)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal("box");
            m_Dic[m_EntityList[i].Key] = EditorGUILayout.Toggle(m_Dic[m_EntityList[i].Key], GUILayout.Width(20));
            GUILayout.Label(m_EntityList[i].Name);
            GUILayout.Label(m_EntityList[i].Tag, GUILayout.Width(100));
            GUILayout.Label(m_EntityList[i].ToPath, GUILayout.Width(100));
            GUILayout.Label(m_EntityList[i].Version.ToString(), GUILayout.Width(100));
            GUILayout.Label(m_EntityList[i].Size.ToString(), GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

          
            foreach (string path in m_EntityList[i].PathList)
            {
                EditorGUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
    }



    /// <summary>
    /// 选定Tag 处理
    /// </summary>
    private void ChooseTagHandler()
    {
        switch (tagIndex)
        {
            case 0: //"All"
                for (int i = 0; i < m_EntityList.Count; i++)
                {
                    m_Dic[m_EntityList[i].Key] = true;
                }
                break;
            case 1: //"Role"
            case 2: //"Scene"
            case 3: //"Effect"
            case 4: //"Audio"
                for (int i = 0; i < m_EntityList.Count; i++)
                {
                    if (m_EntityList[i].Tag.Equals(tagArrs[tagIndex], StringComparison.CurrentCultureIgnoreCase))
                    {
                        m_Dic[m_EntityList[i].Key] = true;
                    }
                    else
                    {
                        m_Dic[m_EntityList[i].Key] = false;
                    }
                }
                break;
            case 5: //"None"
                for (int i = 0; i < m_EntityList.Count; i++)
                {
                    m_Dic[m_EntityList[i].Key] = false;
                }
                break;
        }
    }
    /// <summary>
    /// 选择平台处理
    /// </summary>
    private void ChooseTargetHandler()
    {
        switch (targetIndex)
        {
            case 0: //Window
                target = BuildTarget.StandaloneWindows;
                break;
            case 1://Android 
                target = BuildTarget.Android;
                break;
            case 2://IOS
                target = BuildTarget.iOS;
                break;
        }
        Debug.LogFormat("当前选择的平台为：{0}", targetArrs[targetIndex]);
    }
    /// <summary>
    /// 打包处理
    /// </summary>
    private void BuildHandler()
    {
        List<AssetBundleEntity> needBuildAb = new List<AssetBundleEntity>();
        foreach (AssetBundleEntity entity in m_EntityList)
        {
            //勾选了
            if (m_Dic[entity.Key])
            {
                needBuildAb.Add(entity);
            }
        }
        Debug.Log("打包开始");
        for (int i = 0; i < needBuildAb.Count; i++)
        {
            Debug.LogFormat("当前正在打包{0}/{1}", i + 1, needBuildAb.Count);
            BuildAssetBundle(needBuildAb[i]);
        }
        Debug.Log("打包完成");
    }

    private void BuildAssetBundle(AssetBundleEntity entity)
    {
        AssetBundleBuild[] assbundles = new AssetBundleBuild[1];
        AssetBundleBuild bundle = new AssetBundleBuild();
        //包名
        //bundle.assetBundleName = string.Format("{0}.{1}", entity.Name, entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle");
        bundle.assetBundleName = entity.Name;
        //后缀
        bundle.assetBundleVariant = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle";
        //所有资源名称
        bundle.assetNames = entity.PathList.ToArray();

        assbundles[0] = bundle;

        //保存路径
        string toPath = Application.dataPath + "/../AssetBundles/" + targetArrs[targetIndex] + entity.ToPath;

        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }

        BuildPipeline.BuildAssetBundles(toPath, assbundles, BuildAssetBundleOptions.None, target);
    }
    /// <summary>
    /// 清空目录处理
    /// </summary>
    private void ClearAssetHandler()
    {
        string path = Application.dataPath + "/../AssetBundles/" + targetArrs[targetIndex];
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }
}
