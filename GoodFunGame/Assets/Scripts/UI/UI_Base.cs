using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class UI_Base : MonoBehaviour
{
    private bool _initialized;

    public virtual bool Init()
    {
        if (_initialized) return false;

        _initialized = true;
        return true;
    }


    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();


    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)  // T 에 속하는 오브젝트들을 Dictionary의 Value인 objects 배열의 원소들에 하나하나 추가
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }


    protected void BindObject(Type type) => Bind<GameObject>(type);
    protected void BindText(Type type) => Bind<TextMeshProUGUI>(type);
    protected void BindButton(Type type) => Bind<Button>(type);
    protected void BindImage(Type type) => Bind<Image>(type);


    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); } // 오브젝트로서 가져오기
    protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); } // TextMeshProUGUI //Text로서 가져오기
    protected Button GetButton(int idx) { return Get<Button>(idx); } // Button로서 가져오기
    protected Image GetImage(int idx) { return Get<Image>(idx); } // Image로서 가져오기



    public static void BindEvent(GameObject go, Action<PointerEventData> action = null,  Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case UIEvent.Press:
                evt.OnPressedHandler -= action;
                evt.OnPressedHandler += action;
                break;
            case UIEvent.PointerDown:
                evt.OnPointerDownHandler -= action;
                evt.OnPointerDownHandler += action;
                break;
            case UIEvent.PointerUp:
                evt.OnPointerUpHandler -= action;
                evt.OnPointerUpHandler += action;
                break;
            case UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case UIEvent.BeginDrag:
                evt.OnBeginDragHandler -= action;
                evt.OnBeginDragHandler += action;
                break;
            case UIEvent.EndDrag:
                evt.OnEndDragHandler -= action;
                evt.OnEndDragHandler += action;
                break;
        }
    }

}
