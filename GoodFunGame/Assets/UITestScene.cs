using UnityEngine;

public class UITestScene : MonoBehaviour
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

        // Main.UI.ShowSceneUI<UI_Scene_Select>();

       // Main.UI.ShowSceneUI<UI_Scene_Battle>();


    }
}