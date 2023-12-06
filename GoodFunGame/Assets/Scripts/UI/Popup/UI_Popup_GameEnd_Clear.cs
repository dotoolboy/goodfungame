using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_GameEnd_Clear : UI_Popup
{

    private enum GameObjects
    {
        Stage,
        Score,
        NextBtn,
        UpdateScore

    }

    private enum Buttons
    {
        NextBtn,

    }

    private enum Images
    {
        IconImage,
        Btn
    }

    private enum Texts
    {
        StageNameText,
        StageNumberText,
        CurrentScoreText, 
        HighScoreText
    }


    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetObject((int)GameObjects.UpdateScore).gameObject.SetActive(false);
        GetObject((int)GameObjects.NextBtn).gameObject.SetActive(false);
        GetObject((int)GameObjects.Score).gameObject.SetActive(false);
        GetObject((int)GameObjects.Stage).gameObject.SetActive(false);

        GetButton((int)Buttons.NextBtn).gameObject.BindEvent(NextStage);

        return true;

    }

    public void Open()
    {
        Init();

        GetText((int)Texts.StageNameText).text = Main.Stage.stageName;
        GetText((int)Texts.CurrentScoreText).text = Main.Stage.StageCurrentScore.ToString();
        GetText((int)Texts.HighScoreText).text = Main.Game.Data.stageHighScore.ToString();

        StopAllCoroutines();
        StartCoroutine(PreviewCountdown());
        Time.timeScale = 0;
    }
    IEnumerator PreviewCountdown()
    {
        yield return new WaitForSecondsRealtime(1f);
        GetObject((int)GameObjects.Stage).gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        GetObject((int)GameObjects.Score).gameObject.SetActive(true);

        if (Main.Stage.StageCurrentScore >= Main.Game.Data.stageHighScore)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            GetObject((int)GameObjects.UpdateScore).gameObject.SetActive(true); 
        }

        yield return new WaitForSecondsRealtime(1f);
        GetObject((int)GameObjects.NextBtn).gameObject.SetActive(true);
    }


    void NextStage(PointerEventData data)
    {
        Debug.Log("다음스테이지로 이동");
        Time.timeScale = 1;
        Main.Game.Data.stageLevel++;
        Main.UI.ClosePopupUI(this);

    }

}
