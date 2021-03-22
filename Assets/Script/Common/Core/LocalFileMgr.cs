//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-03-22 16:08:32 
// 版本：1.0 
// 备注：
//***********************************************************
using System.IO;
using UnityEngine;

public class LocalFileMgr : Singleton<LocalFileMgr>
{
#if UNITY_EDITOR
#if UNITY_STANDALONE_WIN
        public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/Window/";
#elif UNITY_ANDROID
    public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/Android/";
#elif UNITY_IPHONE
         public readonly string LocalFilePath = Application.dataPath + "/../AssetBundles/IOS/";
#endif
#elif UNITY_STANDALONE_WIN || UNITY_ANDROID || UNITY_IPHONE
         public readonly string LocalFilePath = Application.persistentDataPath +"/";
#endif

    /// <summary>
    /// 从路径文件中读取byte数组
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public byte[] GetByte(string path)
    {
        byte[] buffer = null;
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
}
