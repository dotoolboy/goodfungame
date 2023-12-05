using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        Right,
        Left,
        Cursor,
    }
    #endregion


    private Animator anim;

    private Animator cursorAnim;


    private Animator leftAnim;
    private Animator rightAnim;


    private bool isTyping = false;
    private bool isClicking; //커서 눌리는 애니 동안 입력안받게

    private float clickDelay;

    private int line;
    private WaitForSeconds typingSpeed = new WaitForSeconds(0.02f); // 타이핑 속도


    [HideInInspector] public Dictionary<int, DialogueSetting> talkData;


    void Start()
    {

        Init();

    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));


        talkData = new Dictionary<int, DialogueSetting> // 실험용, 나중에 json파일로 쓸것
            {
               { 0, new DialogueSetting("테스ㅁㄴㅇㅁㄴㅇㄴ트", "안녕안녕 첫줄입니다?") },
               { 1, new DialogueSetting("테스트", "방가방가 둘째줄입니다!") },
               { 2, new DialogueSetting("테스트", "마지막 대사입니다!!") }
            };

        anim = GetComponent<Animator>();
        

        GetButton((int)Buttons.TalkBtn).gameObject.BindEvent(Talk);

        rightAnim = GetObject((int)GameObjects.Right).GetComponent<Animator>();
        leftAnim = GetObject((int)GameObjects.Left).GetComponent<Animator>();
        cursorAnim = GetObject((int)GameObjects.Cursor).GetComponent<Animator>();
        clickDelay = 0.3f; // 클릭 애니클립 길이 읽고 넣어주기



        Open(null);
        return true;
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

        Talk(null);

    }



    public void Close()
    {
        StopAllCoroutines();
        CancelInvoke();
        isTyping = false;

        anim.SetBool("isOpen", false);

        Invoke("CloseEvent", anim.GetCurrentAnimatorStateInfo(0).length); // 대화창 다 내려가면 UI사라지도록
    }

    public void CloseEvent()
    {
        Main.UI.ClosePopupUI(this);
    }

}
