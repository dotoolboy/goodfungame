using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Temp : MonoBehaviour
{
    private string _sceneName;

    private void LoadResource()
    {
        ResourceManager.Instance.LoadAllAsync<Object>(_sceneName, (key, count, totalCount) =>
        {
            
            // After Resource Load
        });
    }
}
