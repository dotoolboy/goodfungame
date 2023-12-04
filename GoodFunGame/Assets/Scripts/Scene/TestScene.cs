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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < 5; i++)
                Main.Object.Spawn<Enemy>("BOSS_MWJ", new Vector2(Random.Range(-5f, 5f), 5f));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Main.Object.Despawn(FindObjectOfType<Enemy>());
        }
    }

    private void InitializeGame()
    {
        // ====== 게임 시작 ======

        GameObject newObj = Main.Resource.InstantiatePrefab("TempCharacter.prefab");
        Main.Object.Spawn<Player>("Player", Vector2.zero);
        for (int i = 0; i < 5; i++)
            Main.Object.Spawn<Enemy>("BOSS_MWJ", new Vector2(Random.Range(-5f, 5f), 5f));
        // =======================
    }

}