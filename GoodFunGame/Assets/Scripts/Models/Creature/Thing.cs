using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ӿ��� �����ϴ� ��κ��� ������Ʈ�� ���̽��� �Ǵ� Ŭ����.
/// </summary>
public class Thing : MonoBehaviour {

    private bool _initialized;
    protected DataManager DataManager;
    protected virtual void Awake() {
        Initialize();
        DataManager = ServiceLocator.GetService<DataManager>();
    }

    public virtual bool Initialize() {
        if (_initialized) return false;
        _initialized = true;

        return true;
    }

}