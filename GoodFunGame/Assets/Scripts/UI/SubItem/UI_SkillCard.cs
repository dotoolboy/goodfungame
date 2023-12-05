using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillCard : UI_Base
{
    #region Enums
    enum Texts
    {
        Name,
        Introduce,
        Price,

    }
    enum Images
    {
        IconBackground,
        IconImage,
    }
    enum Buttons
    {
        BuyBtn,
    }

    #endregion

    #region Property
    // public Sprite iconBackSprite;   // 아이콘 Background
    public Sprite iconSprite;   // Icon Sprite
    public string skillNameText;    // 스킬 이름
    public string skillDescText;    // 스킬 설명
    public int skillPriceInteger;   // 스킬 가격
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
        BindImage(typeof(Images));
        GetButton((int)Buttons.BuyBtn).gameObject.BindEvent(PurchasePopup);
        Refresh();
        return true;
    }


    void Refresh()
    {
        // 내가가진 돈이 이 스킬 가격보다 적으면 변화 
        // 현재 재산으로 못사는 스킬을 구매버튼 비활성화
        // 구매한 스킬은 구매했다고 표시되야함
        // 해금된 스킬이라면 변화
   
        GetText((int)Texts.Name).text = skillNameText;
        GetText((int)Texts.Introduce).text = skillDescText;
        GetText((int)Texts.Price).text = $"{skillPriceInteger} Gold";
        GetImage((int)Images.IconImage).sprite = iconSprite;
        // GetImage((int)Images.IconBackground).sprite = iconBackSprite;
    }
    void PurchasePopup(PointerEventData data)
    {
        Debug.Log("구매버튼 눌렀습니다"); // 구매버튼 누르면 구매 확인창뜨고 ok 눌러야 구매됨

        Main.UI.ShowPopupUI<UI_Popup_Purchase>();

    }

}

