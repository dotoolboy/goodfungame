using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        // ==================================== 씬 진입 시 처리 ====================================

        UI = Main.UI.ShowSceneUI<UI_Scene_Game>();

        Main.Object.Spawn<Player>("Player", new(0, -2));



        // =========================================================================================

        return true;
    }
}