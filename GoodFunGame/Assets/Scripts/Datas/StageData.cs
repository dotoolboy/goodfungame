using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

[Serializable]
public class StageData
{
    public enum StageCharge
    {
        MWJ,
        CHH,
        LJH,
        JEH,
        KSJ
    }
    public StageCharge stageCharge; // 스테이지 주인
    public string stageChargeKey;   // 스테이지 키
    
    public string[] startDialogue;  // 시작 대사
    public EnemyData.EnemyKey firstAppearanceEnemyKey;  // 1wave 등장 적
    public int firstWave;   // 1 wave 숫자
    
    public string[] secondDialogue; // 2 wave 시작 대사
    public EnemyData.EnemyKey secondAppearanceEnemyKey; // 2 wave 등장 적
    public int secondWave;  // 2wave 숫자

    public string[] thirdDialogue;  // 3 wave 시작 대사
    public EnemyData.EnemyKey thirdAppearanceEnemyKey;  // 3wave 등장 적
    public int thirdWave;   //  3wave 숫자

    public string[] stageClearDialogue; // 승리시 대사
    public string[] stageFailDialogue;  // 실패시 대사
    public int rewardGold;  // 보상 골드
}

[Serializable]
public class StageDataLoader : ILoadData<string, StageData>
{
    public List<StageData> stages = new();

    public Dictionary<string, StageData> MakeData()
    {
        return stages.ToDictionary(stage => stage.stageChargeKey);
    }
}

