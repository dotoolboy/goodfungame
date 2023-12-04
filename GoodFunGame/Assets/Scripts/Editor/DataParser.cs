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
        ParseEnemyData("Enemy");
    }

    private static void ParseEnemyData(string csvFileName)
    {
        EnemyDataLoader loader = new();

        string[] lines = File.ReadAllText($"{Application.dataPath}/@CsvData/{csvFileName}Data.csv").Split("\n");

        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Replace("\r", "").Split(',');
            
            if (row.Length == 0 || string.IsNullOrEmpty(row[0])) continue;
            
            EnemyData.FireType enumFire = (EnemyData.FireType)Enum.Parse(typeof(EnemyData.FireType), row[4]);
            
            loader.enemies.Add(new()
            {
                enemyType = (EnemyData.EnemyKey)Enum.Parse(typeof(EnemyData.EnemyKey), row[0]),
                keyName = Enum.Parse(typeof(EnemyData.EnemyKey), row[0]).ToString(),
                speed = float.Parse(row[1]),
                hp = int.Parse(row[2]),
                damage = int.Parse(row[3]),
                fireType = enumFire.ToString()
            });
        }

        string jsonFile = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@JsonData/{csvFileName}Data.json", jsonFile);
        AssetDatabase.Refresh();
    }
#endif
}