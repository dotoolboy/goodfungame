using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class UI_Popup_IntroMenu : UI_Popup
{
    #region Enums
   
    enum Buttons
    {
        StartButton,
    }

    #endregion
    void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnButtonClicked);

        return true;
    }

    public void OnButtonClicked(PointerEventData data)
    {
        // 셀렉트 씬으로 넘어가기 
        Main.UI.ClosePopupUI(this);
        Main.UI.ShowSceneUI<UI_Scene_Select>();



    }
}
