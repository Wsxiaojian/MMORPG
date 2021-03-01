//***********************************************************
// 描述：主城场景控制
// 作者：fanwei 
// 创建时间：2021-02-23 20:14:33
// 版本：1.0
// 备注：
//***********************************************************
using System;
using UnityEngine;

/// <summary>
/// 主城场景控制
/// </summary>
public class CitySceneCtrl : MonoBehaviour
{
    /// <summary>
    /// 玩家出生点
    /// </summary>
    [SerializeField]
    private Transform m_PlayerBornPos;


    private void Awake()
    {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIType.MainCity);
    }

    void Start()
    {

        GameObject roleGo = RoleMgr.Instance.LoadRole(RoleType.MainPlayer, "Role_MainPlayer");
        //设置再 出生点
        roleGo.transform.position = m_PlayerBornPos.position;
        roleGo.transform.localScale = Vector3.one;

        //角色控制器
        RoleCtrl roleCtrl = roleGo.GetComponent<RoleCtrl>();

        RoleInfo roleInfo = new RoleInfoMainPlayer();
        roleInfo.RoleNickName = GlobalInit.Instance.CurNickName;
        roleInfo.RoleID = 1;
        roleInfo.HpMax = roleInfo.CurHp = 10000;

        roleCtrl.Init(RoleType.MainPlayer, roleInfo, null);

        //全局玩家对象
        GlobalInit.Instance.CurPlayer = roleCtrl;

        FingerEvent.Instance.OnFigerDrag += OnFigerDrag;
        FingerEvent.Instance.OnPlayerClickGround += OnPlayerClickGround;
        FingerEvent.Instance.OnZoom += OnZoom;
    }

    private void Update()
    {

    }
    
    private void OnDestroy()
    {
        FingerEvent.Instance.OnFigerDrag -= OnFigerDrag;
        FingerEvent.Instance.OnPlayerClickGround -= OnPlayerClickGround;
        FingerEvent.Instance.OnZoom -= OnZoom;
    }

    /// <summary>
    /// 手指拖动处理
    /// </summary>
    /// <param name="dragDir"></param>
    private void OnFigerDrag(FingerEvent.DragDir dragDir)
    {
        switch (dragDir)
        {
            case FingerEvent.DragDir.Up:
                CameraCtrl.Instance.SetCameraUp(1);
                break;
            case FingerEvent.DragDir.Down:
                CameraCtrl.Instance.SetCameraUp(0);
                break;
            case FingerEvent.DragDir.Left:
                CameraCtrl.Instance.SetCameraRotate(0);
                break;
            case FingerEvent.DragDir.Right:
                CameraCtrl.Instance.SetCameraRotate(1);
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// 玩家点击地面
    /// </summary>
    private void OnPlayerClickGround()
    {      //移动到目标点
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.name.Equals("Ground", System.StringComparison.CurrentCultureIgnoreCase))
            {
                GlobalInit.Instance.CurPlayer.DoMove(hitInfo.point);
            }
        }
    }

    /// <summary>
    /// 缩放摄像机
    /// 缩放摄像机
    /// </summary>
    /// <param name="zoomType"></param>
    private void OnZoom(FingerEvent.ZoomType zoomType)
    {
        switch (zoomType)
        {
            case FingerEvent.ZoomType.In:
               CameraCtrl.Instance.SetCameraZoom(0);
                break;
            case FingerEvent.ZoomType.Out:
               CameraCtrl.Instance.SetCameraZoom(1);
                break;
            default:
                break;
        }
    }
}
