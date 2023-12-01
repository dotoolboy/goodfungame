using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ӿ��� �����ϴ� ��κ��� ������Ʈ�� ���̽��� �Ǵ� Ŭ����.
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