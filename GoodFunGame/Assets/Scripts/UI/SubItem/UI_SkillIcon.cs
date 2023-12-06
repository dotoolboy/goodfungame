using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SkillIcon : UI_Base
{
    #region Enums
    enum Images
    {
        IconImage,
    }
    #endregion

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindImage(typeof(Images));

        Refresh();

        return true;
    }

    void Refresh()
    {
        // 스킬정보 넣어주기

    }

}
