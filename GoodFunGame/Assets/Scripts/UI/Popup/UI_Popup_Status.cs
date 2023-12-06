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

    // 스킬 장착 관리, 캐릭터 정보 이름 등등등 관리가능해야함
    public override bool Init()
    {
        if (!base.Init()) return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));

        GetText((int)Texts.BestText).text = "최고기록 : 99999999"; 
        GetText((int)Texts.SkillCollectText).text = "수집율100퍼";  //$"수집율 : { 해금된스킬갯수 / Main.Data.Skills.Keys * 100f).ToString()}%";

        GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);
        Refresh();


        return true;
    }
    private void Refresh()
    {
        GetText((int)Texts.GoldText).text = Main.Game.Gold.ToString();
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
