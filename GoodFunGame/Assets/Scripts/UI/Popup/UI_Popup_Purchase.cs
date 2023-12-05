using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Purchase : UI_Popup
{
    #region Enums
    enum Texts
    {
    
    }
    enum Images
    {
    }
    enum Buttons
    {
        OkBtn,
        NoBtn
    }

    #endregion

    #region Fields

    private UI_SkillCard _card;

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

        GetButton((int)Buttons.OkBtn).gameObject.BindEvent(OnBtnOk);
        GetButton((int)Buttons.NoBtn).gameObject.BindEvent(OnBtnNo);

        return true;
    }

    public void SetInfo(UI_SkillCard card)
    {
        _card = card;
    }

    void OnBtnOk(PointerEventData data)
    {
        Main.Game.PurchaseSkill(_card.Data.skillStringKey);
        _card.Refresh();
        Main.UI.ClosePopupUI(this);
    }
    void OnBtnNo(PointerEventData data)
    {
        _card.Refresh();
        Main.UI.ClosePopupUI(this);

    }
}
