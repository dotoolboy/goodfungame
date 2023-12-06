using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class UI_Popup_SkillEquipButton : UI_Popup
{

    #region Enums

    enum GameObjects
    {
    }

    enum Buttons
    {
    }

    #endregion


    void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));



    //    GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);

        return true;
    }

    void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
