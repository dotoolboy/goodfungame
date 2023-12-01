using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임에서 등장하는 대부분의 오브젝트의 베이스가 되는 클래스.
/// </summary>
public class Thing : MonoBehaviour {

    private bool _initialized;

    protected virtual void Awake() {
        Initialize();
    }

    public virtual bool Initialize() {
        if (_initialized) return false;
        _initialized = true;

        return true;
    }

}