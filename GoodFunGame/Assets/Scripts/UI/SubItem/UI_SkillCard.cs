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
        BuyText,
    }
    enum Images
    {
        IconBackground,
        IconImage,
        BuyBtn,

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


        GetButton((int)Buttons.BuyBtn).gameObject.SetActive(true);
        GetButton((int)Buttons.BuyBtn).gameObject.BindEvent(PurchasePopup);
        Main.Game.OnResourcesChanged -= Refresh;
        Main.Game.OnResourcesChanged += Refresh;
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


        if ((GetButton((int)Buttons.BuyBtn) != null) && Main.Game.PurchasedSkills.Contains(Data.skillStringKey))
        {
            GetButton((int)Buttons.BuyBtn).gameObject.SetActive(false);
            GetText((int)Texts.Price).text = "";
        }

        GetText((int)Texts.BuyText).text = Data.skillPrice > Main.Game.Gold ? "소지금 부족" : "구매하기";

        GetButton((int)Buttons.BuyBtn).interactable = Data.skillPrice <= Main.Game.Gold; // 바인딩클릭막는건 이걸론 안된다
        GetImage((int)Images.BuyBtn).raycastTarget = Data.skillPrice <= Main.Game.Gold; // 레이캐스트 끄니까 가능

    }
    void PurchasePopup(PointerEventData data)
    {
        AudioController.Instance.SFXPlay(SFX.Button);
        Main.UI.ShowPopupUI<UI_Popup_Purchase>().SetInfo(this);
    }

}

