using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Status : UI_Popup
{


    #region Enums

    enum Images
    {
        PlayerImage,
    }

    enum Buttons
    {
        BackspaceBtn
    }

    enum Texts
    {
        NameText,
        SkillCollectText,
        BestText,
        GoldText

    }
    enum GameObjects
    {
        Content
    }

    #endregion
    void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));


        GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);
        Refresh();


        return true;
    }
    void Refresh()
    {
        GetText((int)Texts.BestText).text = $"최고기록 : 999999999";
        GetText((int)Texts.SkillCollectText).text = $"수집율 : {String.Format("{0:#}", (float)Main.Game.PurchasedSkills.Count / Main.Data.Skills.Count * 100)}%";
        GetText((int)Texts.GoldText).text = $"{Main.Game.Gold}";
        GetText((int)Texts.NameText).text = $"{Main.Game.UserName}";
        SetSkillEquip();

        
    }

    private void SetSkillEquip()
    {
        foreach (string key in Main.Data.Skills.Keys)
        {
            UI_SkillEquip newEquip = Main.Resource.InstantiatePrefab("UI_SkillEquip.prefab", GetObject((int)GameObjects.Content).transform).GetComponent<UI_SkillEquip>();
            newEquip.SetInfo(key);
        }
    }


    void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
