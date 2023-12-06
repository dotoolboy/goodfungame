using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class UI_Popup_Status : UI_Popup
{

    #region Enums

    enum Images
    {
        PlayerImage,


    }

    // Slider18_Frame 널로 쓸 이미지

    enum Buttons
    {
        BackspaceBtn,
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
        Content,

        Panel


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

        SetSkill();

        Main.Game.OnEquipChanged -= SetSkill;
        Main.Game.OnEquipChanged += SetSkill;
        Refresh();


        return true;
    }
    void Refresh()
    {
        GetText((int)Texts.BestText).text = $"최고기록 : 999999999";
        GetText((int)Texts.SkillCollectText).text = $"수집율 : {String.Format("{0}", (float)Main.Game.PurchasedSkills.Count / Main.Data.Skills.Count * 100)}%";
        GetText((int)Texts.GoldText).text = $"소지금 : {Main.Game.Gold}";
        GetText((int)Texts.NameText).text = $"{Main.Game.UserName}";



    }

    private void SetSkill() // 스킬 목록 만들기
    {
        foreach (string key in Main.Data.Skills.Keys)
        {
            UI_SkillEquip newEquip = Main.Resource.InstantiatePrefab("UI_SkillEquip.prefab", GetObject((int)GameObjects.Content).transform).GetComponent<UI_SkillEquip>();
            newEquip.SetInfo(key);
        }

        for (int i = 0; i < 3; i++)
        {
            UI_MountSkillBtn mount = Main.Resource.InstantiatePrefab("UI_MountSkill.prefab", GetObject((int)GameObjects.Panel).transform).GetComponent<UI_MountSkillBtn>();
                mount.num = i;
        }

    }

    void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
