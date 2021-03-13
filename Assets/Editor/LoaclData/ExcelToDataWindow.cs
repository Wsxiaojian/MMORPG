//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-13 15:23:36 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEditor;
using UnityEngine;

public class ExcelToDataWindow : EditorWindow
{
    private static ExcelToDataWindow window;

    [MenuItem("GameTool/ExcelToData ")]
    private static void  OpenExcelToDataWidnow()
    {
        window = GetWindow(typeof(ExcelToDataWindow), false, "ExcelToData") as ExcelToDataWindow;

        window.minSize = new Vector2(600, 200);
        window.maxSize = new Vector2(800, 400);

        window.Show();
    }

    private string selectFilePath;

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        EditorGUILayout.LabelField("请先选择Excel文件!");
        EditorGUILayout.EndHorizontal();

        //空行
        GUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10);
        selectFilePath = EditorGUILayout.TextField("文件路径:", selectFilePath, GUILayout.MinWidth(400), GUILayout.Height(20));
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(20);
        if (GUILayout.Button("选择Excel文件", GUILayout.Width(130), GUILayout.Height(25)))
        {
            selectFilePath = EditorUtility.OpenFilePanel("选择Excel文件", Application.dataPath, "xls");
        }
        GUILayout.Space(25);
        if (GUILayout.Button("ExcelToData", GUILayout.Width(130), GUILayout.Height(25)))
        {
            if (string.IsNullOrEmpty(selectFilePath))
            {
                //提示 未选择文件 
            }
            else if(selectFilePath.EndsWith(".xls") == false){
                //提示 选择文件格式存在问题

            }
            else
            {
                //转换
                if (ExcelToData.DoExcelToData(selectFilePath))
                {
                    //提示 成功
                }
                else
                {
                    //提示 失败
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        //显示读取的文件信息


        EditorGUILayout.EndVertical();
    }

}

