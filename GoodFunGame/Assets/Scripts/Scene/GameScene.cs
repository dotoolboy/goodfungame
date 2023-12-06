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
        player.ScoreCount = 0;
        player.GoldCount = 0;

        if (_coGame != null) StopCoroutine(_coGame);
        _coGame = StartCoroutine(CoGame());
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

    private IEnumerator CoGame()
    {
        Main.Spawn.StageVolume(1);
        yield return new WaitForSeconds(5.0f);
        Main.Spawn.StageVolume(2);
        yield return new WaitForSeconds(5.0f);
        Main.Spawn.StageVolume(3);
        yield return new WaitForSeconds(5.0f);
        Main.Spawn.StageVolume(3);
        yield return new WaitForSeconds(5.0f);
        Main.Spawn.StageVolume(3);
        yield return new WaitForSeconds(5.0f);
        Main.Spawn.StageVolume(3);
        yield return new WaitForSeconds(5.0f);
        Main.Spawn.StageVolume(4);
    }

    //void OnPause()
    //{
    //    if (Time.timeScale == 0f)
    //    {
    //        // 현재 일시 정지된 상태이므로 게임을 재개합니다.
    //        Time.timeScale = 1f;
    //        Main.UI.CloseAllPopupUI();
    //    }
    //    else
    //    {
    //        // 현재 실행 중인 상태이므로 게임을 일시 정지하고 팝업을 표시합니다.
    //        Time.timeScale = 0f;
    //        Main.UI.ShowPopupUI<UI_Popup_Pause>();
    //    }
    //}
}