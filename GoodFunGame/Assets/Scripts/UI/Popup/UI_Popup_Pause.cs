using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Pause : UI_Popup
{

    #region Enums
    enum Texts
    {
        BackToMainText,
        OptionsText,
        ContinueText,
    }

    enum Buttons
    {
        BackToMainBtn,
        OptionsBtn,
        ContinueBtn
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

        GetButton((int)Buttons.BackToMainBtn).gameObject.BindEvent(BackToMain);
        GetButton((int)Buttons.OptionsBtn).gameObject.BindEvent(Options);
        GetButton((int)Buttons.ContinueBtn).gameObject.BindEvent(Continue);


    }

    public void BackToMain(PointerEventData data)
    {
        Debug.Log("메인으로");
    }
    public void Options(PointerEventData data)
    {
        Debug.Log("옵션");
       
    }
    public void Continue(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
        Debug.Log("게임계속하기");
    }
}
