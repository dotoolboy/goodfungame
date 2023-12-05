using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_BattleMenu : UI_Popup
{

    enum Entry
    {
        Single,
        Multiple
    }

    public override bool Init()
    {
        if (!base.Init()) return false;
        return true;
    }
}
