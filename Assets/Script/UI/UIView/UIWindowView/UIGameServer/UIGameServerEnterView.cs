//***********************************************************
// 描述：
// 作者：fanwei 
// 创建时间：2021-04-14 20:45:59 
// 版本：1.0 
// 备注：
//***********************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameServerEnterView : UIWindowViewBase
{
    /// <summary>
    /// 游戏服名称
    /// </summary>
    [SerializeField]
    private Text Txt_GameServerName;

    /// <summary>
    /// 按钮点击
    /// </summary>
    /// <param name="btnGo"></param>
    protected override void OnBtnClick(GameObject btnGo)
    {
        base.OnBtnClick(btnGo);

        switch (btnGo.name)
        {
            //登陆
            case "Btn_SelectServer":
                UIDispatcher.Instance.Dispatch(ConstDef.UIGameServerEnterView_Btn_SelectServer);
                break;
            // 注册窗口
            case "Btn_EnterGame ":
                //切换到注册窗口
                UIDispatcher.Instance.Dispatch(ConstDef.UIGameServerEnterView_Btn_EnterGame);
                break;
        }
    }


    /// <summary>
    /// 设置UI显示
    /// </summary>
    /// <param name="gameServerName"></param>
    public  void SetUI(string  gameServerName)
    {
        Txt_GameServerName.text = gameServerName;
    }
}
