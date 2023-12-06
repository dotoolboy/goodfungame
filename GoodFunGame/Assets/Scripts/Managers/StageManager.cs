using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager
{
    private bool _stageInit;
    public int stageLevel;
    public int CurrentStageLevel;
    public Dictionary<int, EnemyData.EnemyKey> stageDesign = new();
    public int stageHighScore;
    public int stageCurrentScore;
    private int _waveCount;
    private int[] _waveDesign;

    #region SpawnGroup Controller

    public readonly string[] BossKey = {
        "BOSS_MWJ",
        "BOSS_CHH",
        "BOSS_LJH",
        "BOSS_JEH",
        "BOSS_KSJ"
    };

    public int BossIndex;

    public readonly Dictionary<string, List<string>> EnemyGroups = new()
    {
        {"BOSS_MWJ", new List<string> {"SOLDIER1_MWJ", "SOLDIER2_MWJ", "SOLDIER3_MWJ"}},
        {"BOSS_CHH", new List<string> {"SOLDIER1_CHH", "SOLDIER2_CHH", "SOLDIER3_CHH"}},
        {"BOSS_LJS", new List<string> {"SOLDIER1_LJH", "SOLDIER2_LJH", "SOLDIER3_LJH"}},
        {"BOSS_JEH", new List<string> {"SOLDIER1_JEH", "SOLDIER2_JEH", "SOLDIER3_JEH"}},
        {"BOSS_KSJ", new List<string> {"SOLDIER1_KSJ", "SOLDIER2_KSJ", "SOLDIER3_KSJ"}}
    };
    #endregion

    public void InitializeStage()
    {
        if (_stageInit) return;
        _stageInit = true;
        stageLevel = Main.Game.Data.stageLevel;
        stageHighScore = Main.Game.Data.stageHighScore;
        BossIndex = 0;
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

    private void Stage1(int waveCount)
    { 
        int bossIndex = 0; 
        SpawnDesign(waveCount, bossIndex);
    }

    private void Stage2(int waveCount)
    { 
        int bossIndex = 1; 
        SpawnDesign(waveCount, bossIndex);
    }
    private void Stage3(int waveCount)
    { 
        int bossIndex = 2; 
        SpawnDesign(waveCount, bossIndex);
    }
    private void Stage4(int waveCount)
    { 
        int bossIndex = 3; 
        SpawnDesign(waveCount, bossIndex);
    }
    private void Stage5(int waveCount)
    { 
        int bossIndex = 4; 
        SpawnDesign(waveCount, bossIndex);
    }

    private void OnVictory(int killCount)
    {
        if (killCount != _waveDesign.Sum())
        {
            return;
        }

        Debug.Log("Victory");
            
        // GameScene에서 승리패널 오픈 호출
        if (stageCurrentScore > stageHighScore)
        {
            Main.Game.Data.stageHighScore = stageCurrentScore;
        }

        if (CurrentStageLevel > stageLevel)
        {
            Main.Game.Data.stageLevel = CurrentStageLevel;
        }

    }
}
