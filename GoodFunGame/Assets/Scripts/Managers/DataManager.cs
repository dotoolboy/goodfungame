using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataManager : MonoBehaviour
{
    public Dictionary<string, EnemyData> Enemies = new Dictionary<string, EnemyData>();
    private AddressableAsset _addressableAsset;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void Start()
    {
        _addressableAsset = ServiceLocator.GetService<AddressableAsset>();
    }

    public void InitializeData()
    {
        Enemies = LoadJson<EnemyDataLoader, string, EnemyData>("EnemyData").MakeData();
    }

    private TLoader LoadJson<TLoader, TKey, TValue>(string path) where TLoader : ILoadData<TKey, TValue>
    {
        TextAsset textAsset = _addressableAsset.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<TLoader>(textAsset.text);
    }
}
