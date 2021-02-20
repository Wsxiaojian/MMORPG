using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptCreateInit : UnityEditor.AssetModificationProcessor
{
   public static void  OnWillCreateAsset(string path)
    {
        Debug.Log("111" + path);
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            string strContent =
            "//***********************************************************\r\n"
            + "// 描述：这是一个功能代码\r\n"
            + "// 作者：#AuthorName# \r\n"
            + "// 创建时间：#CreateTime#\r\n"
            + "// 版 本：1.0\r\n"
            + "// 备注：\r\n"
            + "//***********************************************************\r\n";
            strContent = strContent.Replace("#AuthorName#", "fanwei").Replace("#CreateTime#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            strContent += File.ReadAllText(path);
            

            File.WriteAllText(path, strContent);
            //AssetDatabase.Refresh();
        }
    }

}
