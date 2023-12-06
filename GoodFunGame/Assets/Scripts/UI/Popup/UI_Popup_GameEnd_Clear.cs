using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_GameEnd_Clear : UI_Popup
{

    enum GameObjects
    {
        Stage,
        Score,
        NextBtn,
        UpdateScore

    }

    enum Buttons
    {
        NextBtn,

    }

    enum Images
    {
        IconImage,
        Btn
    }

    enum Texts
    {
        StageName,
        StageNumberText,
        

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

        StopAllCoroutines();
        StartCoroutine(PreviewCountdown());

    }
    IEnumerator PreviewCountdown()
    {
        yield return new WaitForSecondsRealtime(1f);
        GetObject((int)GameObjects.Stage).gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        GetObject((int)GameObjects.Score).gameObject.SetActive(true);

        // 최고기록 갱신했으면         GetObject((int)GameObjects.UpdateScore).gameObject.SetActive(true); 글씨 띄우기
        yield return new WaitForSecondsRealtime(1f);

        GetObject((int)GameObjects.NextBtn).gameObject.SetActive(true);

    }


    void NextStage(PointerEventData data)
    {
        
        Debug.Log("다음스테이지로 이동");

        Main.UI.ClosePopupUI(this);
    }

}
