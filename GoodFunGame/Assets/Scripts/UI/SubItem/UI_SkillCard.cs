using System.Collections;
using System.Collections.Generic;
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

    #region Properties
    
    public SkillData Data { get; private set; }

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

    public void SetInfo(string key)
    {
        Data = Main.Data.Skills[key];
        Refresh();
    }

    public void Refresh()
    {
        if (Data == null) return;
        Init();
        GetText((int)Texts.Name).text = Data.skillStringKey;
        GetText((int)Texts.Introduce).text = Data.skillDesc;
        GetText((int)Texts.Price).text = $"{Data.skillPrice} Gold";
        GetImage((int)Images.IconImage).sprite = Main.Resource.Load<Sprite>($"{Data.skillStringKey}.sprite");



        // 내가가진 돈이 이 스킬 가격보다 적으면 변화 
        // 현재 재산으로 못사는 스킬을 구매버튼 비활성화
        // 구매한 스킬은 구매했다고 표시되야함
        // 해금된 스킬이라면 변화
    }
    void PurchasePopup(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Purchase>().SetInfo(this);
    }

}

