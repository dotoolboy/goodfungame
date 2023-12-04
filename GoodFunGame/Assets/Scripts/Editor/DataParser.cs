using System;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

/// <summary>
///  에디터 메뉴 중 Tool 을 선택 후 ParseCSV 를 클릭하면 JSON 데이터 생성
/// </summary>
public class DataParser : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Tools/DeleteGameData")]
    public static void DeleteGameData()
    {
        PlayerPrefs.DeleteAll();
        string path = Application.persistentDataPath + "/SaveData.json";
        if (File.Exists(path)) File.Delete(path);
    }

    [MenuItem("Tools/ParseCSV")]
    public static void ParseCsv()
    {
        ParseData<EnemyDataLoader>("Enemy");
        ParseData<SkillDataLoader>("Skill");
        ParseData<StageDataLoader>("Stage");
    }

    #region Parsing Method
    /// <summary>
    ///  CSV_Data 파싱 하여 JSON에 덮어 쓰기
    /// </summary>
    /// <param name="csvFileName"></param>
    private static void ParseData<T>(string csvFileName) where T :  new()
    {
        T loader = new();
        string[] lines = File.ReadAllText($"{Application.dataPath}/@CsvData/{csvFileName}Data.csv").Split("\n");
        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            if (row.Length == 0 || string.IsNullOrEmpty(row[0])) continue;
            MappingData(csvFileName, row, loader);
        }
        string jsonFile = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@JsonData/{csvFileName}Data.json", jsonFile);
        AssetDatabase.Refresh();
    }
    #endregion

    #region Mapping Method
    /// <summary>
    ///  DataMapping Method
    /// </summary>
    /// <param name="csvFilename"></param>
    /// <param name="row"></param>
    /// <param name="loader"></param>
    /// <typeparam name="T"></typeparam>
    private static void  MappingData<T>(string csvFilename,string[] row, T loader) where T : new()
    {
        switch (csvFilename)
        {
            case "Enemy":
                EnemyData(row, loader as EnemyDataLoader);
                break;
            case "Skill":
                SkillData(row, loader as SkillDataLoader);
                break; 
            case "Stage":
                StageData(row,loader as StageDataLoader);
                break;
        }
    }
    #endregion

    #region EnemyData
    /// <summary>
    ///  EnemyData CSV
    /// </summary>
    /// <param name="row"></param>
    /// <param name="loader"></param>
    private static void EnemyData(string[] row, EnemyDataLoader loader)
    {
        EnemyData.FireType enumFire = (EnemyData.FireType)Enum.Parse(typeof(EnemyData.FireType), row[4]);
        loader.enemies.Add(new EnemyData
        {
            enemyType = (EnemyData.EnemyKey)Enum.Parse(typeof(EnemyData.EnemyKey), row[0]),
            keyName = Enum.Parse(typeof(EnemyData.EnemyKey), row[0]).ToString(),
            speed = float.Parse(row[1]),
            hp = int.Parse(row[2]),
            damage = int.Parse(row[3]),
            fireType = enumFire.ToString()
        });
    }
    #endregion

    #region SkillData
    /// <summary>
    ///  SkillData CSV
    /// </summary>
    /// <param name="row"></param>
    /// <param name="loader"></param>
    private static void SkillData(string[] row, SkillDataLoader loader)
    {
        loader.skills.Add(new SkillData
        {
            skill = (SkillData.Skills)Enum.Parse(typeof(SkillData.Skills), row[0]),
            skillStringKey =   Enum.Parse(typeof(SkillData.Skills), row[0]).ToString(),
            skillType = (SkillData.SkillTypes)Enum.Parse(typeof(SkillData.SkillTypes), row[1]),
            skillLevel = int.Parse(row[2]),
            skillCool = float.Parse(row[3]),
            skillDuration = float.Parse(row[4]),
            skillDamage = int.Parse(row[5]),
            skillPrice = int.Parse(row[6]),
            skillDesc = row[7].Replace("_","\n")
        });
    }
    #endregion

    #region StageData
    private static void StageData(string[] row, StageDataLoader loader)
    {
        string[] startDialogue = row[1].Split('/');
        string[] secondDialogue = row[4].Split('/');
        string[] thirdDialogue = row[7].Split('/');
        string[] stageClearDialogue = row[10].Split('/');
        string[] stageFailDialogue = row[11].Split('/');

        loader.stages.Add(new StageData
        {
            stageCharge = (StageData.StageCharge)Enum.Parse(typeof(StageData.StageCharge), row[0]),
            stageChargeKey = Enum.Parse(typeof(StageData.StageCharge), row[0]).ToString(),
            startDialogue = startDialogue, // Assign the split string array
            firstAppearanceEnemyKey = (EnemyData.EnemyKey)Enum.Parse(typeof(EnemyData.EnemyKey), row[2]),
            firstWave = int.Parse(row[3]),
            secondDialogue = secondDialogue, // Assign the split string array
            secondAppearanceEnemyKey = (EnemyData.EnemyKey)Enum.Parse(typeof(EnemyData.EnemyKey), row[5]),
            secondWave = int.Parse(row[6]),
            thirdDialogue = thirdDialogue, // Assign the split string array
            thirdAppearanceEnemyKey = (EnemyData.EnemyKey)Enum.Parse(typeof(EnemyData.EnemyKey), row[8]),
            thirdWave = int.Parse(row[9]),
            stageClearDialogue = stageClearDialogue, // Assign the split string array
            stageFailDialogue = stageFailDialogue, // Assign the split string array
            rewardGold = int.Parse(row[12])
        });
    }
    #endregion


#endif
}