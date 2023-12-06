using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static SkillData;

public class UI_Popup_SkillEquip : UI_Popup
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
    #region Fields

 //  private 스킬정보 _skill;

    #endregion
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
     //   _skill = card;
    }


    void OnBtnOk(PointerEventData data)
    {
        // 스킬 장착하고 끼고있는 스킬있으면 해제 시키기
        // _skill.Data.skillStringKey;
        //  _skill.Refresh();
        Main.UI.ClosePopupUI(this);
    }
    void OnBtnNo(PointerEventData data)
    {
        //_skill.Refresh();
        Main.UI.ClosePopupUI(this);

    }
}
