using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UI_Popup_Shop : UI_Popup
{

    #region Enums

    private enum Texts
    {
        GoldText,
        PercentText
    }

    private enum Images
    {
        ShopNpc,
    }

    private enum Buttons
    {
        BackspaceBtn,
    }

    private enum GameObjects
    {
        Content,
    }

    #endregion
    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
        GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);
        Refresh();

        return true;
    }
    private void Refresh()
    {
        GetText((int)Texts.GoldText).text = Main.Game.Gold.ToString();
        GetText((int)Texts.PercentText).text = "100%"; // 스킬 해금율 퍼센트
        SetSkillCard();
    }

    private void SetSkillCard()
    {
        foreach (string key in Main.Data.Skills.Keys)
        {
            UI_SkillCard newCard = Main.Resource.InstantiatePrefab("Shop_SkillCard.prefab", GetObject((int)GameObjects.Content).transform).GetComponent<UI_SkillCard>();
            newCard.SetInfo(key);
        }
    }
    public void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
