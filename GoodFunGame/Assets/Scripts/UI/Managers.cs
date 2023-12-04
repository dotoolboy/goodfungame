using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다.




    public static ResourceManager Resource = new ResourceManager();

    public static UIManager UI = new UIManager();


    void Start()
    {
        Init();
    }


    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                //빈오브젝트를 생성한다.
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            // 마음대로 추가하고 삭제할 수 없도록 함.
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
