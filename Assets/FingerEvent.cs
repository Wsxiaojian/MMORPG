using UnityEngine;
using System.Collections;

public class FingerEvent :  MonoBehaviour {


    public static FingerEvent Instance;

    /// <summary>
    /// 手指拖动方向
    /// </summary>
    public enum DragDir
    {
        Up,
        Down,
        Left,
        Right
    }

    /// <summary>
    /// 放大类型
    /// </summary>
    public enum ZoomType
    {
        /// <summary>
        /// 拉近
        /// </summary>
        In,
        /// <summary>
        /// 拉远
        /// </summary>
        Out
    }

    /// <summary>
    /// 前一拖动坐标点
    /// </summary>
    private Vector2 oldDragPos;

    /// <summary>
    /// 点击类型
    /// </summary>
    private int porClickType =-1;

    /// <summary>
    /// 前一双指距离
    /// </summary>
    private float proTouchDis;

    /// <summary>
    /// 手指拖动委托
    /// </summary>
    public System.Action<DragDir> OnFigerDrag;

    /// <summary>
    /// 点击委托
    /// </summary>
    public System.Action OnPlayerClick;

    /// <summary>
    ///  拉近拉远委托
    /// </summary>
    public System.Action<ZoomType> OnZoom;


    private void Awake()
    {
        Instance = this;
    }


    void OnEnable()
    {
    	//启动时调用，这里开始注册手势操作的事件。
    	
    	//按下事件： OnFingerDown就是按下事件监听的方法，这个名子可以由你来自定义。方法只能在本类中监听。下面所有的事件都一样！！！
        FingerGestures.OnFingerDown += OnFingerDown;
        //抬起事件
		FingerGestures.OnFingerUp += OnFingerUp;
	    //开始拖动事件
	    FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
        //拖动中事件...
        FingerGestures.OnFingerDragMove += OnFingerDragMove;
        //拖动结束事件
        FingerGestures.OnFingerDragEnd += OnFingerDragEnd; 
		//长按事件
		FingerGestures.OnFingerLongPress += OnFingerLongPress;
		
    }
    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (OnZoom != null)
            {
                OnZoom(ZoomType.In);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (OnZoom != null)
            {
                OnZoom(ZoomType.Out);
            }
        }
#elif UNITY_ANDROID || UNITY_IPHONE
         if (Input.touchCount > 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                float touchDis = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(0).position);
                if (proTouchDis > touchDis)
                {
                    if (OnZoom != null)
                    {
                        OnZoom(ZoomType.In);
                    }
                }
                else
                {
                    if (OnZoom != null)
                    {
                        OnZoom(ZoomType.Out);
                    }
                }
            }
        }
#endif
    }


        void OnDisable()
    {
    	//关闭时调用，这里销毁手势操作的事件
    	//和上面一样
        FingerGestures.OnFingerDown -= OnFingerDown;
		FingerGestures.OnFingerUp -= OnFingerUp;
		FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= OnFingerDragEnd; 
		FingerGestures.OnFingerLongPress -= OnFingerLongPress;
    }

    //按下时调用
    void OnFingerDown( int fingerIndex, Vector2 fingerPos )
    {
        porClickType = 1;
    }
	
	//抬起时调用
	void OnFingerUp( int fingerIndex, Vector2 fingerPos, float timeHeldDown )
	{
        //上一次是点击
        if(porClickType == 1)
        {
            porClickType = -1;
            if (OnPlayerClick != null)
            {
                OnPlayerClick();
            }
        }
	}
	
	//开始滑动
	void OnFingerDragBegin( int fingerIndex, Vector2 fingerPos, Vector2 startPos )
    {
        porClickType = 2;
        oldDragPos = fingerPos;
    }

	//滑动中
    void OnFingerDragMove( int fingerIndex, Vector2 fingerPos, Vector2 delta )
    {
        porClickType = 3;

        Vector2 dir = fingerPos - oldDragPos;

        DragDir dragDir;
        if (dir.x > dir.y) {
            if(dir.x > -dir.y) {
                //往右
                dragDir = DragDir.Right;
            }
            else {
                //往下
                dragDir = DragDir.Down;
            }
        }
        else{
            if (dir.x > -dir.y) {
                //往左
                dragDir = DragDir.Up;
            }
            else {
                //往上
                dragDir = DragDir.Left;
            }
        }

        if (OnFigerDrag != null)
        {
            OnFigerDrag(dragDir);
        }
    }
    //滑动结束
    void OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
    {
        porClickType = 4;
    }
    //长按事件
    void OnFingerLongPress( int fingerIndex, Vector2 fingerPos )
	{
		
		Debug.Log("OnFingerLongPress " + fingerPos );
	}
}
