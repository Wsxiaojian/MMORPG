//***********************************************************
// 描述：mono 单例
// 作者：fanwei 
// 创建时间：2021-03-21 17:45:13 
// 版本：1.0 
// 备注：
//***********************************************************
using UnityEngine;

/// <summary>
/// mono 单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T :MonoBehaviour
{
    private static T instance;
    /// <summary>
    /// 单例
    /// </summary>
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name);

                instance = obj.GetOrCreateComponent<T>();

                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }


    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        OnUpdate();
    }

    private void OnDestroy()
    {
        BeforeOnDestroy();
    }


    /// <summary>
    /// 子类继承 awake
    /// </summary>
    protected virtual void OnAwake() { }
    /// <summary>
    /// 子类继承 start
    /// </summary>
    protected virtual void OnStart() { }
    /// <summary>
    /// 子类继承 每帧更新
    /// </summary>
    protected virtual void OnUpdate() { }
    /// <summary>
    /// 子类继承 销毁物体之前调用
    /// </summary>
    protected virtual void BeforeOnDestroy() { }

}
