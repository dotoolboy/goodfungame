using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{

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
                    InitializeGame();
                }
            });
        }
    }

    private void InitializeGame()
    {
        // ====== 게임 시작 ======

        GameObject newObj = Main.Resource.InstantiatePrefab("TempCharacter.prefab");




        // =======================
    }

}