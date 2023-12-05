using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_BattleMenu_Multiple : UI_Popup_BattleMenu
{

    #region Enums

    enum Texts
    {
        ScoreText,
        GoldText,

    }

    enum GameObjects
    {
        SkillList,
        KeyList,
        HeartList,

    }
    enum Buttons
    {
        PauseBtn
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
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));


        GetButton((int)Buttons.PauseBtn).gameObject.BindEvent(Pause);

    }

    void Pause(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Pause>();



    }

}
