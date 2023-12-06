using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class StageManager
{
    public int stageLevel;
    public int CurrentStageLevel;
    private int _stageHighScore;
    private int _stageCurrentScore;
    public string stageName;

    public int StageCurrentScore
    {
        get => _stageCurrentScore; 
        set
        {
            _stageCurrentScore = value;
            OnScoreChanged?.Invoke();
        }
    }

    public event Action OnScoreChanged;

    private int _waveCount;
    private int[] _waveDesign;

    #region SpawnGroup Controller

    public readonly Dictionary<string, string> BossKey = new()
    {
        {"BOSS_MWJ", "케로케로폼폼미친원정"}, { "BOSS_CHH", "파인애플피자는맛있현호" },
        {"BOSS_LJH","고기반찬으로만든훈민정훈"}, { "BOSS_JEH"," 작고귀여은하짱" }, { "BOSS_KSJ" ,"다음엔튜터로보세진"}
    };

    public readonly Dictionary<string, List<string>> EnemyGroups = new()
    {
        {"BOSS_MWJ", new List<string> {"SOLDIER1_MWJ", "SOLDIER2_MWJ", "SOLDIER3_MWJ"}},
        {"BOSS_CHH", new List<string> {"SOLDIER1_CHH", "SOLDIER2_CHH", "SOLDIER3_CHH"}},
        {"BOSS_LJH", new List<string> {"SOLDIER1_LJH", "SOLDIER2_LJH", "SOLDIER3_LJH"}},
        {"BOSS_JEH", new List<string> {"SOLDIER1_JEH", "SOLDIER2_JEH", "SOLDIER3_JEH"}},
        {"BOSS_KSJ", new List<string> {"SOLDIER1_KSJ", "SOLDIER2_KSJ", "SOLDIER3_KSJ"}}
    };
    #endregion

    public void InitializeStage()
    {
        stageLevel = Main.Game.Data.stageLevel == 0 ? 1 : Main.Game.Data.stageLevel;
        Main.Game.Data.stageLevel = stageLevel;
        _stageHighScore = Main.Game.Data.stageHighScore;
        StageCurrentScore = 0;
        _waveCount = GameManager.StageWaveMaxCount;
        _waveDesign = Main.Game.WaveVolume;
        Main.Object.OnVictory += OnVictory;
    }

    /// <summary>
    ///  스테이지 생성
    /// </summary>
    /// <param name="stage">int 스테이지 레벨 (1 ~ 5)</param>
    /// <param name="spawnDelay">float 스폰 되는 적의 속도</param>
    /// <returns></returns>
    public IEnumerator CreateStage(int stage, float spawnDelay)
    {
        if (stageLevel > 5)
        {
            Debug.Log("최대 스테이지 달성 더이상 게임이 불가능합니다.");
            yield break;
        }

        Main.Resource.InstantiatePrefab("BackGround.prefab");
        int wave = 0; 
        Debug.Log($"Wave : {wave} / WaveDesignCount : {_waveCount}");
        while ( wave <= _waveCount-1)
        {
            int currentWave = _waveDesign[wave];
            switch (stage)
            {
                case 1:
                    Stage1(currentWave);
                    break;
                case 2:
                    Stage2(currentWave);
                    break;
                case 3:
                    Stage3(currentWave);
                    break;
                case 4:
                    Stage4(currentWave);
                    break;
                case 5:
                    Stage5(currentWave);
                    break;
            }
            wave ++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private static void SpawnDesign(int waveCount, int bossIndex)
    {
        List<Enemy> spawnList  = Main.Spawn.EnemyPickList(waveCount, bossIndex);
        Main.Spawn.AssignmentEnemyPattern(spawnList);
    }

    private void GetStageName(int bossIndex)
    {
        string selectedKey = Main.Stage.BossKey.Keys.ElementAt(bossIndex);
        stageName = Main.Stage.BossKey[selectedKey];
    }

    private void Stage1(int waveCount)
    { 
        int bossIndex = 0;
        GetStageName(bossIndex);
        SpawnDesign(waveCount, bossIndex);
    }

    private void Stage2(int waveCount)
    { 
        int bossIndex = 1; 
        GetStageName(bossIndex);
        SpawnDesign(waveCount, bossIndex);
    }
    private void Stage3(int waveCount)
    { 
        int bossIndex = 2; 
        GetStageName(bossIndex);
        SpawnDesign(waveCount, bossIndex);
    }
    private void Stage4(int waveCount)
    { 
        int bossIndex = 3; 
        GetStageName(bossIndex);
        SpawnDesign(waveCount, bossIndex);
    }
    private void Stage5(int waveCount)
    { 
        int bossIndex = 4; 
        GetStageName(bossIndex);
        SpawnDesign(waveCount, bossIndex);
    }

    private void OnVictory()
    {

        Debug.Log("Victory");
            
        // GameScene에서 승리패널 오픈 호출
        UpdateStage();
        Main.UI.ShowPopupUI<UI_Popup_GameEnd_Clear>().Open();
    }

    public void GameOver()
    {
           UpdateStage();
    }

    private void UpdateStage()
    {
        if (StageCurrentScore > _stageHighScore)
        {
            Main.Game.Data.stageHighScore = StageCurrentScore;
        }
        if (CurrentStageLevel > stageLevel)
        {
            Main.Game.Data.stageLevel = CurrentStageLevel;
        }
    }
}
