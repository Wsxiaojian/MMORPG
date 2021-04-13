//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-13 20:49:14 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEngine;


public class AppLog
{
    /// <summary>
    ///  log日志  普通
    /// </summary>
    /// <param name="message"></param>
    public static void Log(object message)
    {
#if DEBUG_LOG
        Debug.Log(message);
#endif
    }

    /// <summary>
    ///  log日志  错误
    /// </summary>
    /// <param name="message"></param>
    public static void LogError(object message)
    {
#if DEBUG_LOG
        Debug.LogError(message);
#endif
    }
}
