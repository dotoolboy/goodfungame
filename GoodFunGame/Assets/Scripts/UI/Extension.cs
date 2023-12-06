using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UIElements;

public static class Extension
{
    
    // 확장메서드
    public static void BindEvent(this GameObject go, Action<PointerEventData> action = null, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }

    public static bool IsValid(this GameObject obj)
    {
        return obj != null && obj.activeSelf;
    }

    public static bool IsValid(this Thing thing)
    {
        return thing != null && thing.isActiveAndEnabled;
    }

    public static void DestroyChilds(this GameObject obj)
    {
        Transform[] children = new Transform[obj.transform.childCount];
        for (int i = 0; i < obj.transform.childCount; i++)
            children[i] = obj.transform.GetChild(i);
        foreach (Transform child in children)
            Main.Resource.Destroy(child.gameObject);
    }

}
