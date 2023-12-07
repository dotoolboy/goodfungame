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
        icon1,
        icon2,
        icon3,
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
        skillSlotImg1,
        skillSlotImg2,
        skillSlotImg3,
        Content,
    


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
        Main.Game.OnEquipChanged -= Refresh;
       Main.Game.OnEquipChanged += Refresh;

        Refresh();


        return true;
    }
    void Refresh()
    {

        GetText((int)Texts.BestText).text = $"최고기록 : 0";
        GetText((int)Texts.SkillCollectText).text = $"수집율 : {Math.Truncate((double)Main.Game.PurchasedSkills.Count / Main.Data.Skills.Count * 100)}%";
        GetText((int)Texts.GoldText).text = $"소지금 : {Main.Game.Gold}";
        GetText((int)Texts.NameText).text = $"{Main.Game.UserName}";


        if (GetObject((int)GameObjects.skillSlotImg1).gameObject != null) { 
        GetObject((int)GameObjects.skillSlotImg1).gameObject.SetActive(false); // 장비변경 이벤트에 연결하면 장비창나갔다들어올때 null
        GetObject((int)GameObjects.skillSlotImg2).gameObject.SetActive(false);
        GetObject((int)GameObjects.skillSlotImg3).gameObject.SetActive(false);
       
        for (int i = 0; i < Main.Game.EquippedSkills.Count; i++)
        {
            GetImage(i).sprite = Main.Resource.Load<Sprite>($"{Main.Data.Skills[Main.Game.EquippedSkills[i]].skillStringKey}.sprite");
            GetObject(i).gameObject.SetActive(true);
            }
        }


    }

    private void SetSkill() // 스킬 목록 만들기
    {
        GetObject((int)GameObjects.Content).DestroyChilds();
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
