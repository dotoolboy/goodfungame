using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourceManager : MonoBehaviour
{
    public bool Loaded { get; private set; }


    private Dictionary<string, UnityEngine.Object> _resources = new();


    public void LoadAsync<T>(string key, Action<T> callback = null)
        where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            callback?.Invoke(resource as T);
            return;
        }

        string loadKey = key;

        if (key.Contains(".sprite"))
            loadKey = $"{key}[{key.Replace(".sprite", "")}]";

        if (key.Contains(".sprite"))
        {
            var asyncOperation = Addressables.LoadAssetAsync<Sprite>(loadKey);
            asyncOperation.Completed += op =>
            {
                _resources.Add(key, op.Result);
                callback?.Invoke(op.Result as T);
            };
        }
        else
        {
            var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
            asyncOperation.Completed += op =>
            {
                _resources.Add(key, op.Result);
                callback?.Invoke(op.Result as T);
            };
        }
    }

    // Scene - Start()
    public void LoadAllAsync<T>(string label, Action<string, int, int> callback)
        where T : UnityEngine.Object
    {
        var operation = Addressables.LoadResourceLocationsAsync(label, typeof(T));

        operation.Completed += op =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, obj =>
                {
                    loadCount++;
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
            }
        };

        //Loaded = true;
    }

    public T Load<T>(string key) where T : UnityEngine.Object
    {
        if (!_resources.TryGetValue(key, out UnityEngine.Object resource))
            return null;
        return resource as T;
    }

    public void Unload<T>(string key) where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            Addressables.Release(resource);
            _resources.Remove(key);
        }
        else
            Debug.LogError($"Resource Unload {key}");
    }

    // prefab
    //GameObject obj = Thing.Resource.InstantiatePrefab("name.prefab");
    public GameObject InstantiatePrefab(string key)
    {
        GameObject prefab = Load<GameObject>(key);
        if (prefab == null)
        {
            Debug.LogError($"[ResourceManager] Instantiate({key}): Failed to load prefab.");
            return null;
        }

        GameObject obj = UnityEngine.Object.Instantiate(prefab);
        obj.name = prefab.name;
        return obj;
    }

    // 해당 오브젝트를 풀에 돌려놓거나 파괴한다.
    public void Destroy(GameObject obj)
    {
        if (obj == null) return;

        if (Main.Pool.Push(obj)) return;

        UnityEngine.Object.Destroy(obj);
    }
}
