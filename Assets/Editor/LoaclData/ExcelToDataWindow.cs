//***********************************************************
// 描述： 编辑器  将Excel转化为Data相关操作  GameTool菜单下
// 作者：fanwei 
// 创建时间：2021-03-13 15:23:36 
// 版本：1.0 
// 备注：
//***********************************************************
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 编辑器  将Excel转化为Data相关操作  GameTool菜单下
/// </summary>
public class ExcelToDataWindow : EditorWindow
{
    private static ExcelToDataWindow window;

    /// <summary>
    /// 文件路径
    /// </summary>
    private static string DataDirPath {
        get{
            return Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/Assets") + 1) + "DataToExcel/";
       }
     }
    /// <summary>
    /// 自动生成文件夹 路径
    /// </summary>
    private static string CreateDirPath
    {
        get
        {
            return DataDirPath + "/Create/";
        }
    }

    /// <summary>
    /// 数据 脚本的文件夹路径
    /// </summary>
    private static string LocalDataScriptDirPath
    {
        get
        {
            return Application.dataPath + "/Script/Data/LocalData/Create/";
        }
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    [MenuItem("GameTool/ExcelToData ")]
    private static void OpenExcelToDataWidnow()
    {
        window = GetWindow(typeof(ExcelToDataWindow), false, "ExcelToData") as ExcelToDataWindow;

        window.minSize = new Vector2(800, 400);
        window.maxSize = new Vector2(1000, 600);

        window.selectPath = DataDirPath;

        Debug.Log(window.selectPath);

        window.Show();
    }

    /// <summary>
    /// 一键copy Create文件 到工程local文件夹下
    /// </summary>
    [MenuItem("GameTool/CopyCreateAllFile ")]
    private static void CopyCreateAllFile()
    {
        //将CreateDirPath路径移动到
        if (Directory.Exists(CreateDirPath) ==false)
        {
            return;
        }
        ////将LocalDataScriptDirPath文件夹内容删除
        //Directory.Delete(LocalDataScriptDirPath, true);

        //创建文件夹
        if (Directory.Exists(LocalDataScriptDirPath) == false)
        {
            Directory.CreateDirectory(LocalDataScriptDirPath);
        }
        //将CreateDirPath文件夹下 所有文件 copy 到LocalDataScriptDirPath文件夹 
        try
        {
            DirectoryInfo dir = new DirectoryInfo(CreateDirPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
            foreach (FileSystemInfo i in fileinfo)
            {
                File.Copy(i.FullName, LocalDataScriptDirPath + "\\" + i.Name, true);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    /// <summary>
    /// 选择的文件路径
    /// </summary>
    private string selectFilePath;
    /// <summary>
    /// 选择的文件夹路径
    /// </summary>
    private string selectPath;
    /// <summary>
    /// 选择的data文件路径
    /// </summary>
    private string selectDataFilePath;
    /// <summary>
    /// 滑动区域
    /// </summary>
    private Vector2 ScrollPos = Vector2.zero;
    /// <summary>
    /// data文件数据内容
    /// </summary>
    private string dataStr = string.Empty;


    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        //-------------------------------------ExcelTodata转换区域------------------------------------
        #region ExcelTodata转换区域
        //提示显示
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.LabelField("请先选择Excel文件!");
        EditorGUILayout.EndHorizontal();

        //文件路径显示
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.TextField("文件路径:", selectFilePath, GUILayout.MinWidth(400), GUILayout.Height(20));
        GUILayout.Space(10);
        EditorGUILayout.EndHorizontal();

        //按钮
        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        if (GUILayout.Button("选择Excel文件", GUILayout.Width(130), GUILayout.Height(25)))
        {
            if (string.IsNullOrEmpty("selectPath"))
            {
                selectFilePath = EditorUtility.OpenFilePanel("选择Excel文件", Application.dataPath, "xls");
            }
            else
            {
                selectFilePath = EditorUtility.OpenFilePanel("选择Excel文件", selectPath, "xls");
            }
            selectPath = selectFilePath.Substring(0, selectFilePath.LastIndexOf('/') + 1);
        }
        GUILayout.Space(25);
        if (GUILayout.Button("ExcelToData", GUILayout.Width(130), GUILayout.Height(25)))
        {
            if (string.IsNullOrEmpty(selectFilePath))
            {
                //提示 未选择文件 
                EditorUtility.DisplayDialog("错误", " 未选择文件！！！", "确定");
            }
            else if (selectFilePath.EndsWith(".xls") == false)
            {
                //提示 选择文件格式存在问题'
                EditorUtility.DisplayDialog("错误", " 选择文件格式存在问题！！！", "确定");
            }
            else
            {
                //转换
                if (ExcelToData.DoExcelToData(selectFilePath))
                {
                    //提示 成功
                    EditorUtility.DisplayDialog("结果", " 转换成功！！！", "确定");
                }
                else
                {
                    //提示 失败
                    EditorUtility.DisplayDialog("结果", " 转换失败！！！", "确定");
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        //------------------------------------------------------------------------------------------------

        GUILayout.Space(20);

        //-------------------------------------显示data文件内容区域------------------------------------
        #region 显示data文件内容区域
        //提示信息
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.LabelField("请先选择Data文件!");
        EditorGUILayout.EndHorizontal();

        //显示读取的文件信息
        GUILayout.Space(5);
        ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.MaxWidth(750), GUILayout.MaxHeight(300));
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(25);
        EditorGUILayout.TextArea(dataStr);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        //按钮
        GUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        if (GUILayout.Button("查看Data文件", GUILayout.Width(130), GUILayout.Height(25)))
        {
            dataStr = string.Empty;
            if (string.IsNullOrEmpty(selectPath))
            {
                selectDataFilePath = EditorUtility.OpenFilePanel("选择需要查看的Data文件", Application.dataPath, "data");
            }
            else
            {
                selectDataFilePath = EditorUtility.OpenFilePanel("选择需要查看的Data文件", selectPath, "data");
            }

            if (string.IsNullOrEmpty(selectDataFilePath))
            {
                //提示 未选择文件 
                EditorUtility.DisplayDialog("错误", " 未选择文件！！！", "确定");
            }
            else if (selectDataFilePath.EndsWith(".data") == false)
            {
                //提示 选择文件格式存在问题
                EditorUtility.DisplayDialog("错误", " 选择文件格式存在问题！！！", "确定");
            }
            else
            {
                dataStr = ExcelToData.DoViewData(selectDataFilePath);
            }
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        //------------------------------------------------------------------------------------------------

        GUILayout.Space(20);

        EditorGUILayout.EndVertical();
    }

}

