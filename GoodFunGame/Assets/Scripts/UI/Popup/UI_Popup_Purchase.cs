using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Purchase : UI_Popup
{
    #region Enums
    enum Texts
    {
    
    }
    enum Images
    {
    }
    enum Buttons
    {
        OkBtn,
        NoBtn
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
        BindImage(typeof(Images));


        GetButton((int)Buttons.OkBtn).gameObject.BindEvent(Ok);
        GetButton((int)Buttons.NoBtn).gameObject.BindEvent(No);



    }

    void Ok(PointerEventData data)
    {
        Debug.Log("샀어요");
        Main.UI.ClosePopupUI(this);

    }
    void No(PointerEventData data)
    {
        Debug.Log("안살래요");
        Main.UI.ClosePopupUI(this);

    }
}
