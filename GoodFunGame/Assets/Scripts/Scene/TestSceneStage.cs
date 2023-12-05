using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneStage : MonoBehaviour
{
    GameObject _gameObject;
    void Start()
    {
        if (Main.Resource.Loaded)
        {
            Main.Data.Initialize();
        }
        else
        {
            Main.Resource.LoadAllAsync<UnityEngine.Object>("PreLoad", (key, count, totalCount) =>
            {
                Debug.Log($"[TestScene] Load asset {key} ({count} / {totalCount})");
                if (count >= totalCount)
                {
                    Main.Data.Initialize();
                    Main.Game.Initialize();
                    InitializeGame();
                }
            });
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
        
    }

    private void InitializeGame()
    {
        // ====== 게임 시작 ======

        GameObject newObj = Main.Resource.InstantiatePrefab("TempCharacter.prefab");
        newObj.transform.position = new Vector3(0f, -3.5f, 0f);
        GameObject stageSprite = Main.Resource.InstantiatePrefab("BackGround.prefab");
        //Main.Object.Spawn<Player>("Player", Vector2.zero);
        //for (int i = 0; i < 5; i++)
        //    Main.Object.Spawn<Enemy>("BOSS_MWJ", new Vector2(Random.Range(-5f, 5f), 5f));
        // =======================

        
    }

    void OnPause()
    {
        if (Time.timeScale == 0f)
        {
            // 현재 일시 정지된 상태이므로 게임을 재개합니다.
            Time.timeScale = 1f;
            Main.UI.CloseAllPopupUI();
        }
        else
        {
            // 현재 실행 중인 상태이므로 게임을 일시 정지하고 팝업을 표시합니다.
            Time.timeScale = 0f;
            Main.UI.ShowPopupUI<UI_Popup_Pause>();
        }
    }



}