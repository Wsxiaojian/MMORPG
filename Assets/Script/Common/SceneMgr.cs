//***********************************************************
// 描述：场景管理器
// 作者：fanwei 
// 创建时间：2021-02-23 09:54:17
// 版本：1.0
// 备注：
//***********************************************************
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
   
    /// <summary>
    /// 当前场景
    /// </summary>
    public SceneType CurSceneType
    {
        get;
        private set;
    }

    /// <summary>
    ///  加载登陆场景
    /// </summary>
    public void LoadLogOn()
    {
        CurSceneType = SceneType.LogOn;
        SceneManager.LoadScene("Scene_Loading");
    }


    /// <summary>
    /// 加载主城场景
    /// </summary>
    public  void LoadMainCity()
    {
        CurSceneType = SceneType.MainCity;
        SceneManager.LoadScene("Scene_Loading");
    }
}
