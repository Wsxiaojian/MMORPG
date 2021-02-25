//***********************************************************
// 描述：UI层级管理
// 作者：fanwei 
// 创建时间：2021-02-23 09:46:12
// 版本：1.0
// 备注：
//***********************************************************
using UnityEngine;

/// <summary>
/// UI层级管理 用于设置窗口层级
/// </summary>
public class LayerUIMgr : Singleton<LayerUIMgr>
{
    /// <summary>
    /// 初始 layerIndex
    /// </summary>
    private int m_CurLayerIndex = 50;

    /// <summary>
    /// 检查当前打开窗口数量
    /// </summary>
    public void CheckOpenWindow()
    {
        if(WindowUIMgr.Instance.OpenWindowNum == 0)
        {
            ResetLayer();
        }
    }

    /// <summary>
    /// 重置层级
    /// </summary>
    public void ResetLayer()
    {
        m_CurLayerIndex = 50;
    }


    /// <summary>
    /// 设置UI层级
    /// </summary>
    /// <param name="obj"></param>
    public void SetLayer(GameObject obj)
    {
        Canvas[]  canvas= obj.GetComponentsInChildren<Canvas>();

        if (canvas != null)
        {
            for (int i = 0; i < canvas.Length; i++)
            {
                //+= 加等
                canvas[i].sortingOrder += m_CurLayerIndex;
            }
        }

        m_CurLayerIndex++;
    }
  
}
