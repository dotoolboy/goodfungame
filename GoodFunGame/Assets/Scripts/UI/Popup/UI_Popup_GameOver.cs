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
        HighScoreText
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
        GetText((int)Texts.HighScoreText).text = $"{Main.Game.Data.stageHighScore}";
        GetText((int)Texts.ScoreText).text = $"{Main.Stage.StageCurrentScore}";
        GetText((int)Texts.GoldText).text = $"{Main.Game.Data.gold}";
    }


    void Retry(PointerEventData data)
    {
        Main.Game.Data.stageLevel = 0;
        Main.UI.ClosePopupUI(this);
        Main.Scene.LoadScene("GameScene");
    }

     void Exit(PointerEventData data)
    {
        Main.Game.Data.stageLevel = 0;
        Main.UI.ClosePopupUI(this);
        Main.Scene.LoadScene("TitleScene");
    }

}
