using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager
{
    public Dictionary<string, EnemyData> Enemies = new();
    public Dictionary<string, SkillData> Skills = new();

    public void Initialize()
    {
        Enemies = LoadJson<EnemyDataLoader, string, EnemyData>("EnemyData").MakeData();
        Skills = LoadJson<SkillDataLoader, string, SkillData>("SkillData").MakeData();
    }

    private TLoader LoadJson<TLoader, TKey, TValue>(string path) where TLoader : ILoadData<TKey, TValue>
    {
        TextAsset textAsset = Main.Resource.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<TLoader>(textAsset.text);
    }
}

#region Use To Data

// Enemy Data Access   Json List Type Data
// Main.Instance.data.Enemies

#endregion