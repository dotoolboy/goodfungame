using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillEquip : UI_Base
{

    #region Enums

    enum GameObjects
    {
        OutLine
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

    #endregion

    #region Properties

    public SkillData Data { get; private set; }
    public bool IsPurchased { get; private set; }
    public bool IsEquipped { get; private set; }

    #endregion

    #region MonoBehaviours

    private void Start()
    {
        Init();
    }

    #endregion

    
    public override bool Init()
    {
        if (!base.Init()) return false;
        
        // Binding.
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        GetButton((int)Buttons.Btn).gameObject.BindEvent(OnBtn);

        Refresh();

        return true;
    }
    public void SetInfo(string key)
    {
        Init();
        Data = Main.Data.Skills[key];
        Refresh();
    }


    public void Refresh()
    {
        if (Data == null) return;

        GetImage((int)Images.IconImage).sprite = Main.Resource.Load<Sprite>($"{Data.skillStringKey}.sprite");

        IsPurchased = Main.Game.PurchasedSkills.Contains(Data.skillStringKey);
        IsEquipped = Main.Game.EquippedSkills.Contains(Data.skillStringKey);

        GetButton((int)Buttons.Btn).interactable = IsPurchased; // 소유한 스킬일때만 버튼 활성화
        GetImage((int)Images.Btn).raycastTarget = IsPurchased;
        GetText((int)Texts.BtnText).text = IsPurchased ? (IsEquipped ? "장착중" : "장착하기") : "미획득";
        GetObject((int)GameObjects.OutLine).gameObject.SetActive(IsEquipped);

    }
    
    private void OnBtn(PointerEventData data)
    {
        if (!IsPurchased) return;

        if (IsEquipped)
        {
            if (!Main.Game.UnequipSkill(Data.skillStringKey)) return;
            Refresh();
        }
        else
        {
            if (!Main.Game.EquipSkill(Data.skillStringKey)) return;
            Refresh();
        }
    }
}
