using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataManager
{
    public Dictionary<string, EnemyData> Enemies = new Dictionary<string, EnemyData>();

    public void Initialize()
    {
        Enemies = LoadJson<EnemyDataLoader, string, EnemyData>("EnemyData").MakeData();
    }

    private TLoader LoadJson<TLoader, TKey, TValue>(string path) where TLoader : ILoadData<TKey, TValue>
    {
        TextAsset textAsset = Main.Resource.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<TLoader>(textAsset.text);
    }
}
