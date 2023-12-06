using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        Init();
        // GetText((int)Texts.ScoreText).text = $"{Main.Object.Player.ScoreCount}";
        // GetText((int)Texts.GoldText).text = $"{Main.Object.Player.GoldCount}";
    }


    void Retry(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
        Main.Clear();
        SceneManager.LoadScene("GameScene");
    }

     void Exit(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
        Main.Clear();
        SceneManager.LoadScene("TitleScene");
    }

}
