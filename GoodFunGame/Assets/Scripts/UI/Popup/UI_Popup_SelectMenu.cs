using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_SelectMenu : UI_Popup
{

    #region Enums

    enum Texts
    {
        ShopBtnText,
        StatusBtnText,
        PlayBtnText,
        OptionBtnText,
        IntroduceText,

    }
 
    enum Buttons
    {
        ShopBtn,
        StatusBtn,
        OptionBtn,
        PlayBtn,

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
        BindText(typeof(Texts));

        GetButton((int)Buttons.ShopBtn).gameObject.BindEvent(Shop);
        GetButton((int)Buttons.StatusBtn).gameObject.BindEvent(Status);
        GetButton((int)Buttons.OptionBtn).gameObject.BindEvent(Option);
        GetButton((int)Buttons.PlayBtn).gameObject.BindEvent(Play);

        return true;
    }

    public void Shop(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Shop>();
    }
    public void Status(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Status>();
    }
    public void Option(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Option>();
    }
    public void Play(PointerEventData data)
    {

        Debug.Log("게임시작");
        // 게임씬으로 이동




    }
}
