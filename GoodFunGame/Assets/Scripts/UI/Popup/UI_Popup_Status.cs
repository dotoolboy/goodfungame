using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Status : UI_Popup
{


    #region Enums
    enum Texts
    {
       
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

    // 스킬 장착 관리, 캐릭터 정보 이름 등등등 관리가능해야함
    public override bool Init()
    {
        if (!base.Init()) return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);

        return true;
    }

    void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
