//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-13 19:58:46 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEngine;
using UnityEngine.iOS;

/// <summary>
/// 设备相关
/// </summary>
public class DeviceUtil 
{

    /// <summary>
    /// 设备唯一标识
    /// </summary>
    public static string DeviceUniqueIdentifier
    {
        get
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }

    /// <summary>
    /// 设备型号
    /// </summary>
    public static  string DeviceModel
    {
        get
        {
#if UNITY_IPHONE && !UNITY_EDITOR
            return  Device.generation.ToString();
#else
            return SystemInfo.deviceModel;
#endif
        }
    }

}
