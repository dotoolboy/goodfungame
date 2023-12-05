using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_SelectEntry : UI_Popup
{

    #region Enums
    enum Texts
    {
    }

    enum Buttons
    {
        Select_Btn_Solo,
        Select_Btn_Multiple
    }
 
    #endregion
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.Select_Btn_Solo).gameObject.BindEvent(Solo);
        GetButton((int)Buttons.Select_Btn_Multiple).gameObject.BindEvent(Multi);


    }

    public void Solo(PointerEventData data)
    {
        Debug.Log("솔플");
        Main.UI.ClosePopupUI(this);
        Main.UI.ShowPopupUI<UI_Popup_SelectMenu>();
    }
    public void Multi(PointerEventData data)
    {
        Debug.Log("멀티플");
        Main.UI.ClosePopupUI(this);
        Main.UI.ShowPopupUI<UI_Popup_SelectMenu>();
    }
}
