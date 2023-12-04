using System;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main instance;
    private static bool initialized;
    public static Main Instance
    {
        get
        {
            if (!initialized)
            {
                initialized = true;

                GameObject obj = GameObject.Find("@Main");
                if (obj == null)
                {
                    obj = new() { name = @"Main" };
                    obj.AddComponent<Main>();
                    DontDestroyOnLoad(obj);
                    instance = obj.GetComponent<Main>();
                }
            }
            return instance;
        }
    }

    private PoolManager pool = new();
    private ResourceManager resource = new();
    private ObjectManager objects = new();
    private DataManager data = new();
    private UIManager ui = new();

    public static PoolManager Pool => Instance?.pool;
    public static ResourceManager Resource => Instance?.resource;
    public static ObjectManager Object => Instance?.objects;
    public static DataManager Data => Instance?.data;
    public static UIManager UI => Instance?.ui;

}