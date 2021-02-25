//***********************************************************
// 描述：主城场景控制
// 作者：fanwei 
// 创建时间：2021-02-23 20:14:33
// 版本：1.0
// 备注：
//***********************************************************
using UnityEngine;

/// <summary>
/// 主城场景控制
/// </summary>
public class CitySceneCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIType.MainCity);
    }
}
