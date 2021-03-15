using System;
using System.IO;

/// <summary>
/// cs文件自动生成标注信息
/// </summary>
public class ScriptCreateInit : UnityEditor.AssetModificationProcessor
{
    /// <summary>
    /// 不需要创建标注的排除路径
    /// </summary>
    private static string[] m_ExcludePath =
    {
        //本地数据 自动创建  DBModel 和 Entity
        "Script/Data/LocalData/Create/",

    };

   public static void  OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs"))
        {
            //先判断是否是排除路径
            if (IsExcludePath(path)) return;

            string strContent =
            "//***********************************************************\r\n"
            + "// 描述：\r\n"
            + "// 作者：#AuthorName# \r\n"
            + "// 创建时间：#CreateTime# \r\n"
            + "// 版本：1.0 \r\n"
            + "// 备注：\r\n"
            + "//***********************************************************\r\n";
            strContent = strContent.Replace("#AuthorName#", "fanwei").Replace("#CreateTime#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            strContent += File.ReadAllText(path);
            

            File.WriteAllText(path, strContent);
        }
    }

    /// <summary>
    /// 判断路径是否是排除路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static bool  IsExcludePath(string path)
    {
        for (int i = 0; i < m_ExcludePath.Length; i++)
        {
            if (path.Contains(m_ExcludePath[i]))
            {
                return true;
            }
        }
        return false;
    }
}
