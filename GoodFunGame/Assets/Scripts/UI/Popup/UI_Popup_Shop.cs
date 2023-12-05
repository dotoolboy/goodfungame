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
        GetText((int)Texts.GoldText).text = "내소지금";
        GetText((int)Texts.PercentText).text = "100%"; // 스킬 해금율 퍼센트
        InstantiateSkillObject("Shop_SkillCard");
    }

    /// <summary>
    ///  스킬 오브젝트 인스턴스화
    /// </summary>
    /// <param name="skillName"></param>
    private void InstantiateSkillObject(string skillName = null)
    {
        if (string.IsNullOrEmpty(skillName))
            skillName = nameof(UI_SkillCard);

        foreach (KeyValuePair<string, SkillData> skillData in Main.Data.Skills)
        {
            GameObject skillCard= Main.Resource.InstantiatePrefab($"{skillName}.prefab", GetObject((int)GameObjects.Content).gameObject.transform);
            UI_SkillCard skillObject = skillCard.GetComponent<UI_SkillCard>();
            Sprite icon = Main.Resource.Load<Sprite>($"{skillData.Key}.sprite");
            if (skillData.Key != icon.name)
            {
                continue;
            }
            skillObject.iconSprite = icon;
            skillObject.skillNameText = skillData.Key;
            skillObject.skillDescText = skillData.Value.skillDesc;
            skillObject.skillPriceInteger = skillData.Value.skillPrice;
        }
    }
    public void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
