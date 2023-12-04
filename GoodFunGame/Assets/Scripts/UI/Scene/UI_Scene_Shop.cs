using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Scene_Shop : UI_Scene
{
    #region Enums
    enum Texts
    {
        SkillCollectText,
        PercentText,
    }
    enum Images
    {
        Background,
        ShopNpc,
    }
    enum Buttons
    {
        BackspaceBtn,
    }
    enum GameObjects
    {
        Content,

    }

    #endregion
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));


        // UI_SkillCard 프리팹 내용을 데이터 읽고 갱신후  GetObject((int)GameObjects.Content).gameObject.transform  자식으로 넣을것
        // 해금된 스킬은 해금상태로 바꾸기



        GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(OnButtonClicked);

        GetText((int)Texts.SkillCollectText).text = "스킬 해금율 :";
        GetText((int)Texts.PercentText).text = "100%"; // 스킬 해금율 퍼센트

    }

    public void OnButtonClicked(PointerEventData data)
    {
        Debug.Log("뒤로가기 버튼");
    }
}
