using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SkillEquip : UI_Base
{

    public SkillData Data { get; private set; }
    enum GameObjects
    {
    }

    enum Buttons
    {
        Btn
    }

    enum Images
    {
        IconImage,
        Btn
    }

    enum Texts
    {
        BtnText,


    }

    private string name;
    private string description;

    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.Btn).gameObject.BindEvent(Equip);


        Main.Game.OnEquipChanged -= Refresh;
        Main.Game.OnEquipChanged += Refresh;

        Refresh();

        return true;
    }
    public void SetInfo(string key)
    {
        Data = Main.Data.Skills[key];
        Init();
        Refresh();
    }

    public void Refresh()
    {
        if (Data == null) return;

        GetImage((int)Images.IconImage).sprite = Main.Resource.Load<Sprite>($"{Data.skillStringKey}.sprite");

        name = Data.skill.ToString(); // 툴팁이 읽는용
        description = Data.skillDesc;

        GetButton((int)Buttons.Btn).interactable = Main.Game.PurchasedSkills.Contains(Data.skillStringKey); // 소유한 스킬일때만 버튼 활성화
        GetImage((int)Images.Btn).raycastTarget = Main.Game.PurchasedSkills.Contains(Data.skillStringKey);
        GetText((int)Texts.BtnText).text = Main.Game.PurchasedSkills.Contains(Data.skillStringKey) ? "장착" : "미획득";

        if (Main.Game.EquipSkills.Contains(Data.skillStringKey))
        {
            GetText((int)Texts.BtnText).text = "착용중";
        }


    }
    void Equip(PointerEventData data)
    {
        if (Main.Game.EquipSkills.Contains(Data.skillStringKey))
        {
            Debug.Log("이미 장착중입니다!");
            return;
        }

        if (Main.Game.EquipSkills.Count >= 3)
        {
            Debug.Log("스킬창이 꽉 찼습니다!!");
            return;
        }


        Debug.Log("스킬 장착했습니다!");
        Main.Game.EquipSkills.Add(Data.skillStringKey);

    }

}
