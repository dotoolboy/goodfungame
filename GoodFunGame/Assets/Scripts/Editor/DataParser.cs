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
    [MenuItem("Tools/ParseCSV")]
    public static void ParseCsv()
    {
        ParseData<EnemyDataLoader>("Enemy");
        ParseData<SkillDataLoader>("Skill");
    }

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

    private static void  MappingData<T>(string csvFilename,string[] row, T loader) where T : new()
    {
        switch (csvFilename)
        {
            case "Enemy":
                EnemyData(row, loader);
                break;
            case "Skill":
                SkillData(row, loader);
                break;
        }
    }

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

    private static void SkillData(string[] row, SkillDataLoader loader)
    {
        string tempString = row[7];
        string replaceText = tempString.Replace("_","\n");
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
            skillDesc =  replaceText,
        });
    }
#endif
}