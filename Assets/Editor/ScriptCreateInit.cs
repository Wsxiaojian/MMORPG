using System;
using System.IO;
using UnityEditor;

public class ScriptCreateInit : UnityEditor.AssetModificationProcessor
{
   public static void  OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
          
            string strContent =
            "//***********************************************************\r\n"
            + "// ����������һ�����ܴ���\r\n"
            + "// ���ߣ�#AuthorName# \r\n"
            + "// ����ʱ�䣺#CreateTime#\r\n"
            + "// �� ����1.0\r\n"
            + "// ��ע��\r\n"
            + "//***********************************************************\r\n";
            strContent += File.ReadAllText(path);
            strContent = strContent.Replace("#AuthorName#", "fanwei").Replace("#CreateTime#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            File.WriteAllText(path, strContent);
            AssetDatabase.Refresh();
        }
    }
}
