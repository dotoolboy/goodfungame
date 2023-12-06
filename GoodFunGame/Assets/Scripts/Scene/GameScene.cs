using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private Coroutine _coGame;

    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        // ==================================== 씬 진입 시 처리 ====================================

        UI = Main.UI.ShowSceneUI<UI_Scene_Game>();
        GameObject stageSprite = Main.Resource.InstantiatePrefab("BackGround.prefab");

        // 플레이어 생성
        Player player = Main.Object.Spawn<Player>("Player", new(0, -2));

        // 스테이지 생성
        if (_coGame != null) StopCoroutine(_coGame);
        // 스테이지 레벨 및 적 스폰 시간 통제
        _coGame = StartCoroutine(Main.Stage.CreateStage(1,5f));

        // =========================================================================================

        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0) Main.UI.CloseAllPopupUI();
            else Main.UI.ShowPopupUI<UI_Popup_Pause>();
        }

    }

}