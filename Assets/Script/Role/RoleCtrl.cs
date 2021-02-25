//***********************************************************
// 描述：角色控制
// 作者：fanwei 
// 创建时间：2021-02-13 12:01:20
// 版 本：1.0
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RoleCtrl : MonoBehaviour
{
    CharacterController m_CharacterController;

    private Vector3 m_TargetPos;
    private Quaternion m_TargetRotate;
    [SerializeField]
    private float m_MoveSpeed;

    private float m_RotateSpeed;

    /// <summary>
    /// 动画机
    /// </summary>
    public Animator Animator;
    
    /// <summary>
    /// 角色状态机
    /// </summary>
    private RoleFSMMgr m_RoleFSMMgr;
    /// <summary>
    /// 角色Ai控制
    /// </summary>
    private IRoleAI m_RoleAI;
    /// <summary>
    /// 角色信息
    /// </summary>
    private RoleInfo m_RoleInfo;



    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();

        m_MoveSpeed = 5;
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.touchCount == 1)
        {

            //移动到目标点
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo))
            {
                if (hitInfo.collider.gameObject.name.Equals("Ground",System.StringComparison.CurrentCultureIgnoreCase))
                {
                    m_TargetPos = hitInfo.point;
                    m_RotateSpeed = 0;
                }
            }
        }

        if (m_CharacterController.isGrounded == false)
        {
            m_CharacterController.Move(transform.position + new Vector3(0, -100, 0) - transform.position);
        }


        if(m_TargetPos!= Vector3.zero)
        {
            if (Vector3.Distance(transform.position, m_TargetPos) > 0.1f)
            {
                //方向
                Vector3 direction = (m_TargetPos - transform.position).normalized;
                direction.y = 0f;
                direction = direction * Time.deltaTime * m_MoveSpeed;

                //转向
                if (m_RotateSpeed < 1)
                {
                    m_RotateSpeed += 5f * Time.deltaTime;
                    m_TargetRotate = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_TargetRotate, m_RotateSpeed);
                }

                //移动
                m_CharacterController.Move(direction);
            }
        }

        CameraMove();
    }

    private void CameraMove()
    {
        if (CameraCtrl.Instance == null) return;

        CameraCtrl.Instance.transform.position = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            CameraCtrl.Instance.SetCameraUp(0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CameraCtrl.Instance.SetCameraUp(1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            CameraCtrl.Instance.SetCameraRotate(0);
          
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CameraCtrl.Instance.SetCameraRotate(1);
        }
        else if (Input.GetKey(KeyCode.I))
        {
            CameraCtrl.Instance.SetCameraZoom(0);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            CameraCtrl.Instance.SetCameraZoom(1);
        }
    }



    public void Init(RoleInfo roleInfo, IRoleAI roleAI)
    {
        m_RoleInfo = roleInfo;
        m_RoleAI = roleAI;

        m_RoleFSMMgr = new RoleFSMMgr(this);
    }

}
