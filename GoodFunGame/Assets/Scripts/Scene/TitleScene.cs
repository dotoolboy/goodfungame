using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override bool Initialize()
    {
        if (!base.Initialize()) return false;

        // ==================================== 씬 진입 시 처리 ====================================

        // 이 곳에 씬 처리 작업!
        UI = Main.UI.ShowSceneUI<UI_Scene_Title>();

        Main.Game.Gold = 600;

        // =========================================================================================

        return true;
    }
}