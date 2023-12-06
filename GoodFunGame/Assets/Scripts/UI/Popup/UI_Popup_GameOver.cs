using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_GameOver : UI_Popup
{

    #region Enums
    enum Buttons
    {
        RetryBtn,
        ExitBtn
    }
    enum Texts
    {
        ScoreText,
        GoldText,
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

        GetButton((int)Buttons.RetryBtn).gameObject.BindEvent(Retry);
        GetButton((int)Buttons.ExitBtn).gameObject.BindEvent(Exit);


        return true;
    }

    public void SetInfo()
    {
        GetText((int)Texts.ScoreText).text = $"{Main.Object.Player.ScoreCount}";
        GetText((int)Texts.GoldText).text = $"{Main.Object.Player.GoldCount}";

    }


    void Retry(PointerEventData data)
    {
        Debug.Log("재도전");
        Main.UI.ClosePopupUI(this);
    }

     void Exit(PointerEventData data)
    {

        Debug.Log("나가기");
        Main.UI.ClosePopupUI(this);
    }

}
