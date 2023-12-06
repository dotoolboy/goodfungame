using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Scene_Game : UI_Scene
{
    #region Enums
    enum Texts
    {
        ScoreText,
        GoldText,
    }
    enum Buttons
    {
        PauseBtn,
    }

    #endregion

    #region Fields

    private Player player;

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

        GetButton((int)Buttons.PauseBtn).gameObject.BindEvent(OnBtnPause);

        player = Main.Object.Player;
        player.cbOnPlayerDataUpdated += OnPlayerDataUpdated;


        Refresh();


        return true;
    }

    private void Refresh()
    {
        GetText((int)Texts.GoldText).text = player.GoldCount.ToString();
        GetText((int)Texts.ScoreText).text = player.ScoreCount.ToString();
    }

    private void OnBtnPause(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Pause>();
    }

    private void OnPlayerDataUpdated()
    {
        GetText((int)Texts.ScoreText).text = player.ScoreCount.ToString();
        GetText((int)Texts.GoldText).text = player.GoldCount.ToString();
    }
}