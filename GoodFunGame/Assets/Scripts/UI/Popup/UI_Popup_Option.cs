using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class UI_Popup_Option : UI_Popup
{

    #region Enums
    enum Texts
    {
        All_NameText,
        All_PercentText,
        BGM_NameText,
        BGM_PercentText,
        SFX_NameText,
        SFX_PercentText,

    }

    enum GameObjects
    {
        All_Slider,
        All_MuteToggle,
        BGM_Slider,
        BGM_MuteToggle,
        SFX_Slider,
        SFX_MuteToggle


    }

    enum Buttons
    {
        BackspaceBtn
    }



    #endregion
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);

    }

    void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
        Debug.Log("옵션창꺼집니다");
    }
}
