using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Shop : UI_Popup
{
    #region Enums
    enum Texts
    {
        GoldText,
        PercentText
    }
    enum Images
    {
        ShopNpc,
    }
    enum Buttons
    {
        BackspaceBtn,
    }
    enum GameObjects
    {
        Content,

    }

    #endregion
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));


        GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);

        // UI_SkillCard 프리팹에 스킬 데이터넣고 GetObject((int)GameObjects.Content).gameObject.transform  자식으로 추가


        Refresh();

    }
    private void Refresh()
    {
        GetText((int)Texts.GoldText).text = "내소지금";
        GetText((int)Texts.PercentText).text = "100%"; // 스킬 해금율 퍼센트
    }


    public void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
