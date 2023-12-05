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
    void Start()
    {
        Init();
    }

    public override void Init()
    {  
        
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.BuyBtn).gameObject.BindEvent(PurchasePopup);
        Refresh();
    }


    void Refresh()
    {
        // 내가가진 돈이 이 스킬 가격보다 적으면 변화 
        // 현재 재산으로 못사는 스킬을 구매버튼 비활성화
        // 구매한 스킬은 구매했다고 표시되야함
        // 해금된 스킬이라면 변화
   
        GetText((int)Texts.Name).text = "스킬이름";
        GetText((int)Texts.Introduce).text = "스킬설명";
        GetText((int)Texts.Price).text = "1000원";

        GetImage((int)Images.IconImage).sprite = null;
        GetImage((int)Images.IconBackground).sprite = null;


    }
    void PurchasePopup(PointerEventData data)
    {
        Debug.Log("구매버튼 눌렀습니다"); // 구매버튼 누르면 구매 확인창뜨고 ok 눌러야 구매됨

        Main.UI.ShowPopupUI<UI_Popup_Purchase>();

    }

}

