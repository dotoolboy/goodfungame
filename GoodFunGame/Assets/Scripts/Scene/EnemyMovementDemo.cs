using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementDemo : MonoBehaviour
{
    void Start()
    {
        if (Main.Resource.Loaded)
        {
            Main.Data.Initialize();
        }
        else
        {
            Main.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
            {
                Debug.Log($"[TestScene] Load asset {key} ({count} / {totalCount})");
                if (count < totalCount)
                {
                    return;
                }
                Main.Data.Initialize();
                Main.Game.Initialize();
                InitializeGame();
            });
        }

    }

    private void InitializeGame()
    {
        // ====== 게임 시작 ======
        // BackGround Start
        // Main.Resource.InstantiatePrefab("EnemyMoveManager.prefab");
        Main.Resource.InstantiatePrefab("BackGround.prefab");
        // Enemy Spawn Start
        // stage Number 입력시 해당 보스 순서에 맞는 적이 스폰 됨
        Main.Spawn.StageVolume(4);
        // =======================
    }
}
