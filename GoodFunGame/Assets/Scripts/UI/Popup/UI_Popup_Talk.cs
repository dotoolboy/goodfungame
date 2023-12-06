using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static StageData;

public class UI_Popup_Talk : UI_Popup
{
    #region Enums
    enum Texts
    {
        LineText,
        NameText

    }
    enum Images
    {
        NameImage,
        LineImage

    }
    enum Buttons
    {
        TalkBtn,
    }
    enum GameObjects
    {
        Character,
        Cursor,
    }

    public enum Dialogue
    {
        WAVE,
        CLEAR,
        FAIL
    }

    #endregion


    #region Field


    private Animator anim;
    private Animator cursorAnim;
    private Animator characterAnim;


    private bool isTyping = false;
    private bool isClicking; //커서 눌리는 애니 동안 입력안받게

    private float clickDelay;

    private int line;
    private WaitForSeconds typingSpeed = new WaitForSeconds(0.02f); // 타이핑 속도


    private Dictionary<int, DialogueSetting> talkData;


    private Dictionary<Dialogue, Dictionary<int, DialogueSetting>> mWJ = new Dictionary<Dialogue, Dictionary<int, DialogueSetting>>();
    private Dictionary<Dialogue, Dictionary<int, DialogueSetting>> cHH = new Dictionary<Dialogue, Dictionary<int, DialogueSetting>>();
    private Dictionary<Dialogue, Dictionary<int, DialogueSetting>> lJH = new Dictionary<Dialogue, Dictionary<int, DialogueSetting>>();
    private Dictionary<Dialogue, Dictionary<int, DialogueSetting>> jEH = new Dictionary<Dialogue, Dictionary<int, DialogueSetting>>();
    private Dictionary<Dialogue, Dictionary<int, DialogueSetting>> kSJ = new Dictionary<Dialogue, Dictionary<int, DialogueSetting>>();



    #endregion



    #region DialogueSpeedSetting
    void DialogueData()
    {

        // 하드코딩 죄송합니다 ㅠㅠ

        mWJ[Dialogue.WAVE] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("문원정", "안녕안녕 첫줄입니다?") },
               { 1, new DialogueSetting("문원정", "방가방가 둘째줄입니다!") },
               { 2, new DialogueSetting("문원정", "마지막 대사입니다!!") }
            };

        mWJ[Dialogue.CLEAR] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("문원정", "졌다!!") },
               { 1, new DialogueSetting("문원정", "쥐엔장~!") },
               { 2, new DialogueSetting("문원정", "재도전하자!") }
            };
        mWJ[Dialogue.FAIL] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("문원정", "승리했다!!") },
               { 1, new DialogueSetting("문원정", "축하합니다!") }
            };

        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■


        cHH[Dialogue.WAVE] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("최현호", "안녕안녕 첫줄입니다?") },
               { 1, new DialogueSetting("최현호", "방가방가 둘째줄입니다!") },
               { 2, new DialogueSetting("최현호", "마지막 대사입니다!!") }
            };

        cHH[Dialogue.CLEAR] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("최현호", "졌다!!") },
               { 1, new DialogueSetting("최현호", "쥐엔장~!") },
               { 2, new DialogueSetting("최현호", "재도전하자!") }
            };
        cHH[Dialogue.FAIL] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("최현호", "승리했다!!") },
               { 1, new DialogueSetting("최현호", "축하합니다!") }
            };

        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

        lJH[Dialogue.WAVE] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("이정훈", "안녕안녕 첫줄입니다?") },
               { 1, new DialogueSetting("이정훈", "방가방가 둘째줄입니다!") },
               { 2, new DialogueSetting("이정훈", "마지막 대사입니다!!") }
            };

        lJH[Dialogue.CLEAR] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("이정훈", "졌다!!") },
               { 1, new DialogueSetting("이정훈", "쥐엔장~!") },
               { 2, new DialogueSetting("이정훈", "재도전하자!") }
            };
        lJH[Dialogue.FAIL] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("이정훈", "승리했다!!") },
               { 1, new DialogueSetting("이정훈", "축하합니다!") }
            };

        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

        jEH[Dialogue.WAVE] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("전은하", "안녕안녕 첫줄입니다?") },
               { 1, new DialogueSetting("전은하", "방가방가 둘째줄입니다!") },
               { 2, new DialogueSetting("전은하", "마지막 대사입니다!!") }
            };

        jEH[Dialogue.CLEAR] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("전은하", "졌다!!") },
               { 1, new DialogueSetting("전은하", "쥐엔장~!") },
               { 2, new DialogueSetting("전은하", "재도전하자!") }
            };
        jEH[Dialogue.FAIL] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("전은하", "승리했다!!") },
               { 1, new DialogueSetting("전은하", "축하합니다!") }
            };

        // ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

        kSJ[Dialogue.WAVE] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("김세진", "안녕안녕 첫줄입니다?") },
               { 1, new DialogueSetting("김세진", "방가방가 둘째줄입니다!") },
               { 2, new DialogueSetting("김세진", "마지막 대사입니다!!") }
            };

        kSJ[Dialogue.CLEAR] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("김세진", "졌다!!") },
               { 1, new DialogueSetting("김세진", "쥐엔장~!") },
               { 2, new DialogueSetting("김세진", "재도전하자!") }
            };
        kSJ[Dialogue.FAIL] = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting("김세진", "승리했다!!") },
               { 1, new DialogueSetting("김세진", "축하합니다!") }
            };

    }

    #endregion

    //  대화창 호출방법 :  Main.UI.ShowPopupUI<UI_Popup_Talk>().DialogueOpen(StageCharge.MWJ, UI_Popup_Talk.Dialogue.FAIL);



    public override bool Init()
    {
        if (!base.Init()) return false;

        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        DialogueData();

        //  대화창 열릴때 스테이지 몇이고 무슨상황인지


        anim = GetComponent<Animator>();


        GetButton((int)Buttons.TalkBtn).gameObject.BindEvent(Talk);

        characterAnim = GetObject((int)GameObjects.Character).GetComponent<Animator>();
        cursorAnim = GetObject((int)GameObjects.Cursor).GetComponent<Animator>();
        clickDelay = 0.3f; // 클릭 애니클립 길이 읽고 넣어주기

        return true;
    }


    public void DialogueOpen(StageCharge name, Dialogue stage)
    {
        Init();


        switch (name)
        {
            case StageCharge.MWJ:
                talkData = mWJ[stage];
                break;
            case StageCharge.CHH:
                talkData = cHH[stage];
                break;
            case StageCharge.LJH:
                talkData = lJH[stage];
                break;
            case StageCharge.JEH:
                talkData = jEH[stage];
                break;
            case StageCharge.KSJ:
                talkData = kSJ[stage];
                break;
        }

        Open(null);
    }



    public void Talk(PointerEventData data)
    {
        if (isClicking) return;


        if (isTyping)  //대사 흐르는중에 클릭하면 대사 다뜸
        {
            CancelInvoke();
            StopAllCoroutines();
            GetText((int)Texts.LineText).maxVisibleCharacters = 999;
            line += 1;

            isTyping = false;

            cursorAnim.gameObject.SetActive(true);

            return;
        }

        isClicking = true; // 커서클릭 애니 진행중
        cursorAnim.SetTrigger("Click");
        Invoke("TalkEvent", clickDelay); // 커서 클릭애니 끝난후에 실행

    }




    public void TalkEvent()
    {
        isClicking = false;

        if (!talkData.ContainsKey(line))
        {
            Close();
            return;
        }

        cursorAnim.gameObject.SetActive(false);


        if (GetText((int)Texts.NameText).text != talkData[line].name)
            NameChange(talkData[line].name);


        GetText((int)Texts.LineText).text = talkData[line].line;

        StartCoroutine(TextVisible());
    }


    private IEnumerator TextVisible()
    {
        isTyping = true;

        GetText((int)Texts.LineText).ForceMeshUpdate();

        GetText((int)Texts.LineText).maxVisibleCharacters = 0;
        yield return new WaitForSeconds(0.02f); // 첫대사 안뜨는 오류 잡는용


        int totalVisibleCharacters = GetText((int)Texts.LineText).textInfo.characterCount;
        int counter = 0;


        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            GetText((int)Texts.LineText).maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters) // 현재줄 대사 다 나왔으면
            {
                line += 1;
                isTyping = false;
                cursorAnim.gameObject.SetActive(true);
                break;
            }

            counter += 1;
            yield return typingSpeed;
        }
    }



    public void NameChange(string name) // 이름에 맞게 이름칸 사이즈 늘리기
    {
        GetText((int)Texts.NameText).text = name;
        GetImage((int)Images.NameImage).rectTransform.sizeDelta = new Vector2(GetText((int)Texts.NameText).preferredWidth + 120f, GetImage((int)Images.NameImage).rectTransform.sizeDelta.y);

    }



    public void Open(PointerEventData data)
    {
        cursorAnim.gameObject.SetActive(false);
        isClicking = false;
        isTyping = false;
        GetText((int)Texts.LineText).text = "";
        line = 0;

        NameChange(talkData[0].name);

        anim.SetBool("isOpen", true);
        characterAnim.SetBool("isTalk", true);

        Talk(null);

    }



    public void Close()
    {
        StopAllCoroutines();
        CancelInvoke();
        isTyping = false;

        anim.SetBool("isOpen", false);
        characterAnim.SetBool("isTalk", false);

        Invoke("CloseEvent", anim.GetCurrentAnimatorStateInfo(0).length); // 대화창 다 내려가면 UI사라지도록
    }

    public void CloseEvent()
    {
        Main.UI.ClosePopupUI(this);
    }



}
