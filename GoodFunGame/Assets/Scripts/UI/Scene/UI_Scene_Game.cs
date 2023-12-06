using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Scene_Game : UI_Scene
{
    #region Enums

    enum Buttons
    {
        PauseBtn,
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

        GetButton((int)Buttons.PauseBtn).gameObject.BindEvent(OnBtnPause);

        return true;
    }

    private void OnBtnPause(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Pause>();
    }
}