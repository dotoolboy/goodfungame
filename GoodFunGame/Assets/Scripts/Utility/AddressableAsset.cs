using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class AddressableAsset : MonoBehaviour
{
    private Dictionary<string, Object> resources = new();
    private DataManager _dataManager;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    private void Start()
    {
        _dataManager = ServiceLocator.GetService<DataManager>();

        LoadAllAsync<Object>("PreLoad", (s, i, arg3) =>
        {
            _dataManager.InitializeData();
        });
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : Object
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
    }

    private void LoadAsync<T>(string key, Action<T> cb = null) where T : Object
    {
        if (resources.TryGetValue(key, out Object resource))
        {
            cb?.Invoke(resource as T);
            return;
        }

        string loadKey = key;

        AsyncOperationHandle<T> asyncResource = Addressables.LoadAssetAsync<T>(loadKey);

        asyncResource.Completed += asyncOperationHandle =>
        {
            resources.Add(key, asyncOperationHandle.Result);
            cb?.Invoke(asyncOperationHandle.Result);
        };
    }

    public T Load<T>(string key) where T : Object
    {
        if (!resources.TryGetValue(key, out Object resource)) return null;
        return resource as T;
    }
}