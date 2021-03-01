//***********************************************************
// 描述：摄像机控制
// 作者：fanwei 
// 创建时间：2021-02-17 11:03:46
// 版 本：1.0
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机控制
/// </summary>
public class CameraCtrl : MonoBehaviour
{
    /// <summary>
    /// 单例
    /// </summary>
    public static CameraCtrl Instance;


    /// <summary>
    /// 摄像机 上下 
    /// </summary>
    [SerializeField]
    private Transform m_CameraUpAndDown;

    /// <summary>
    /// 摄像机 放大缩小
    /// </summary>
    [SerializeField]
    private Transform m_CameraZoomContainer;

    /// <summary>
    /// 摄像机 容器
    /// </summary>
    [SerializeField]
    private Transform m_CameraContainer;


    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 设置摄像机左右旋转
    /// </summary>
    /// <param name="type">type = 0 表示向左  type =1 表示向右</param>
    public void SetCameraRotate(int type)
    {
        transform.Rotate(0, 40f * Time.deltaTime * (type == 0 ? -1:1), 0);
    }

    /// <summary>
    /// 设置摄像机上下移动
    /// </summary>
    /// <param name="type">type = 0 表示向上  type =1 表示向下</param>
    public  void SetCameraUp(int type)
    {
        m_CameraUpAndDown.Rotate(0, 0, 30f * Time.deltaTime * (type == 0 ? 1 : -1));
        //-15,40
        m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, 30, 80));
    }

    /// <summary>
    /// 设置摄像机前后 移动
    /// </summary>
    /// <param name="type">type = 0 表示向前  type =1 表示向后</param>
    public void SetCameraZoom(int type)
    {
        m_CameraContainer.Translate(Vector3.forward * 10 * (type == 0 ? 1 : -1) * Time.deltaTime);

        m_CameraContainer.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(m_CameraContainer.transform.localPosition.z, -5, 5));
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15);


        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 14);

        Gizmos.color = Color.green; 
        Gizmos.DrawWireSphere(transform.position, 12);
    }

}
