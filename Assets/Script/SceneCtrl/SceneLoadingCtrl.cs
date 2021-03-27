//***********************************************************
// 描述：loading场景管理
// 作者：fanwei 
// 创建时间：2021-02-23 13:46:53
// 版本：1.0
// 备注：
//***********************************************************
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// loading场景管理
/// </summary>
public class SceneLoadingCtrl : MonoBehaviour
{
    /// <summary>
    /// loadUi控制
    /// </summary>
    [SerializeField]
    private UISceneLoadingView m_UISceneLoadingView;


    private AsyncOperation m_AsyncOpt;

    /// <summary>
    /// 当前进度
    /// </summary>
    private int m_CurProcess;

    private void Start()
    {
        m_CurProcess = 0;

        DelegateDef.Instance.OnSceneLoadOk += OnSceneLoadOk;

        StartCoroutine(loadNextScene());
    }

    private void OnDestroy()
    {
        DelegateDef.Instance.OnSceneLoadOk -= OnSceneLoadOk;
    }

    /// <summary>
    /// 删除自身
    /// </summary>
    private void OnSceneLoadOk()
    {
        //SceneManager.UnloadSceneAsync("Scene_Loading");

        DestroyImmediate(m_UISceneLoadingView.gameObject);
        DestroyImmediate(gameObject);
    }

    /// <summary>
    /// 加载下一个场景
    /// </summary>
    /// <returns></returns>
    IEnumerator loadNextScene()
    {
        string nextSceneName = string.Empty;
        switch (SceneMgr.Instance.CurSceneType)
        {
            case SceneType.LogOn:
                nextSceneName = "Scene_LogOn";
                break;
            case SceneType.MainCity:
                nextSceneName = "GameScene_1";
                break;
        }

        m_AsyncOpt = SceneManager.LoadSceneAsync(nextSceneName,LoadSceneMode.Additive);
        m_AsyncOpt.allowSceneActivation = false;

        yield return m_AsyncOpt;
    }



    private void Update()
    {
        int process;
        if (m_AsyncOpt.progress < 0.9f)
        {
            process = (int)(m_AsyncOpt.progress *100);
        }
        else
        {
            process = 100;
        }

        if(m_CurProcess < process)
        {
            m_CurProcess++;
        }
        else
        {
            m_CurProcess = process;
            if (process == 100)
            {
                m_AsyncOpt.allowSceneActivation = true;
            }
        }

        //设置进度
        m_UISceneLoadingView.SetProcess(m_CurProcess/100f);
    }


}
