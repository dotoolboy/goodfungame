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

    enum GameObjects
    {
        Skill_1_Key,
        Skill_2_Key,
        Skill_3_Key,
        ControllerIcon

    }

    #endregion

    #region Fields

    private Player player;

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
        BindObject(typeof(GameObjects));
        GetButton((int)Buttons.PauseBtn).gameObject.BindEvent(OnBtnPause);
        Main.Game.OnResourcesChanged += OnPlayerDataUpdated;
        Main.Stage.OnScoreChanged += OnPlayerDataUpdated;
        Refresh();

        return true;
    }

    private void Refresh()
    {
        GetText((int)Texts.GoldText).text = Main.Game.Data.gold.ToString();
        GetText((int)Texts.ScoreText).text = Main.Game.Data.stageHighScore.ToString();


        GetObject((int)GameObjects.Skill_1_Key).gameObject.SetActive(false);
        GetObject((int)GameObjects.Skill_2_Key).gameObject.SetActive(false);
        GetObject((int)GameObjects.Skill_3_Key).gameObject.SetActive(false);
        GetObject((int)GameObjects.ControllerIcon).gameObject.SetActive(false);

        for (int i = 0; i < Main.Game.EquippedSkills.Count; i++)
        {
            GetObject((int)GameObjects.ControllerIcon).gameObject.SetActive(true);
            GetObject(i).gameObject.SetActive(true);
        }


    }

    private void OnBtnPause(PointerEventData data)
    {
        Main.UI.ShowPopupUI<UI_Popup_Pause>();
    }

    private void OnPlayerDataUpdated()
    {
        GetText((int)Texts.ScoreText).text = Main.Stage.StageCurrentScore.ToString();
        GetText((int)Texts.GoldText).text = Main.Game.Data.gold.ToString();
    }
}