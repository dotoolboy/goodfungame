using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SkillCard : UI_Base
{
    #region Enums
    enum Texts
    {
        Name,
        Explanation,
        Price,
        BuyText,

    }
    enum Images
    {
        Panel,
        IconBackground,
        IconImage,
    }
    enum Buttons
    {
        BuyBtn,
    }

    #endregion
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));


        GetButton((int)Buttons.BuyBtn).gameObject.BindEvent(Buy);

        GetText((int)Texts.Name).text = "스킬이름";
        GetText((int)Texts.Explanation).text = "강력한 스킬설명이다";
        GetText((int)Texts.Price).text = "1000원";

        GetText((int)Texts.BuyText).text = "구매";


    }

    void Buy(PointerEventData data)
    {
        Debug.Log("구매버튼 눌렀습니다");
        Main.UI.ShowPopupUI<UI_Popup_Purchase>();


    }

    // 구매버튼 누르면 구매 확인창뜨고 ok 눌러야 구매됨
    // 현재 재산으로 못사는 스킬을 구매버튼 비활성화


}
