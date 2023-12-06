using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    #region Player Info

    public string userName = "Player";
    public int gold = 0;

    public List<string> purchasedSkills = new();    // 구입한 스킬의 skillStringKey를 저장.

    #endregion

    #region Settings Info

    public bool muteBGM = false;
    public bool muteSFX = false;

    #endregion

    #region StageData
    public int stageLevel;
    public int stageHighScore;
    #endregion


}