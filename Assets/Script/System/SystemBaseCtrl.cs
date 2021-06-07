//***********************************************************
// 描述： 系统控制基类
// 作者：fanwei 
// 创建时间：2021-04-14 10:48:00 
// 版本：1.0 
// 备注：
//***********************************************************
using System;

/// <summary>
/// 系统控制基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SystemBaseCtr<T> : IDisposable where T : new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }


    /// <summary>
    /// 消除接口
    /// </summary>
    public virtual void Dispose() { }


    /// <summary>
    /// 打开提示信息显示
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="messageViewType"></param>
    /// <param name="onOk"></param>
    /// <param name="onCancel"></param>
    protected void ShowMessage(string title, string content, MessageViewType messageViewType = MessageViewType.MVT_OK, Action onOk = null, Action onCancel = null)
    {
        MessageCtrl.Instance.Show(title, content, messageViewType, onOk, onCancel);
    }


    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="key"></param>
    /// <param name="onAction"></param>
    protected void AddListenter(string key, DispatcherBase<UIDispatcher, string, object[]>.OnActionHandle onAction)
    {
        UIDispatcher.Instance.AddListenter(key, onAction);
    }

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="key"></param>
    /// <param name="onAction"></param>
    protected void RemoveListenter(string key, DispatcherBase<UIDispatcher, string, object[]>.OnActionHandle onAction)
    {
        UIDispatcher.Instance.RemoveListenter(key, onAction);
    }

    /// <summary>
    /// 日志
    /// </summary>
    /// <param name="message"></param>
    protected void Log(object message)
    {
        AppLog.Log(message);
    }

    /// <summary>
    ///  log日志  错误
    /// </summary>
    /// <param name="message"></param>
    protected void LogError(object message)
    {
        AppLog.LogError(message);
    }
}