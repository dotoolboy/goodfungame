using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 0f;

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
        SceneManager.LoadScene("UI_Scene_Title");
    }
    public void Options(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Option>();

    }
    public void Continue(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
        Time.timeScale = 1f;

    }
}
