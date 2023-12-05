using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Pause : UI_Popup
{

    #region Enums
    enum Texts
    {
     
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

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.BackToMainBtn).gameObject.BindEvent(BackToMain);
        GetButton((int)Buttons.OptionsBtn).gameObject.BindEvent(Options);
        GetButton((int)Buttons.ContinueBtn).gameObject.BindEvent(Continue);

        return true;
    }

    public void BackToMain(PointerEventData data)
    {
        // 셀렉트 씬으로 돌아가기

        Debug.Log("셀렉트 씬으로 돌아가기");
    }
    public void Options(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Option>();

    }
    public void Continue(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
