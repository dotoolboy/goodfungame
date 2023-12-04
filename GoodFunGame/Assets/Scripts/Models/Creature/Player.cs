using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature {

    #region Properties

    // Inputs.
    public Vector2 Input { get; protected set; }
    //public Vector2 Velocity { get; protected set; }

    // Status.
    public float ExpMultiplier { get; protected set; }
    public float CollectDistance => 4.0f;   // TODO:: NO HARDCODING!

    // State.
    public int Level { get; private set; } = 1;
    public float Exp {
        get => _exp;
        set {
            _exp += (value - _exp) * ExpMultiplier;
            // ==================== 레벨업 처리 ====================
            int level = Level;
            while (true) {
                float requiredExp = Temp_GetRequiredExp(level + 1);
                if (requiredExp < 0 || _exp < requiredExp) break;
                level++;
            }
            if (level != Level) {
                Level = level;
                cbOnPlayerLevelUp?.Invoke();
            }
            // =====================================================

            cbOnPlayerDataUpdated?.Invoke();
        }
    }
    public float ExpRatio {
        get {
            float requiredExp = Temp_GetRequiredExp(Level + 1);
            if (requiredExp < 0) return 0;
            float currentTotalExp = Temp_GetRequiredExp(Level);
            return (Exp - currentTotalExp) / (requiredExp -  currentTotalExp);
        }
    }
    public int KillCount {
        get => _killCount;
        set {
            _killCount = value;
            cbOnPlayerDataUpdated?.Invoke();
        }
    }

    #endregion

    #region Fields

    // State.
    private float _exp;
    private int _killCount;
    [SerializeField] private float _speed;

    // Callbacks.
    public Action cbOnPlayerLevelUp;
    public Action cbOnPlayerDataUpdated;

    #endregion

    #region MonoBehaviours

    private void Start()
    {
        MoveSpeed = _speed;
    }

    protected virtual void FixedUpdate()
    {
        _rigidbody.velocity = Direction * MoveSpeed * Time.fixedDeltaTime;
    }

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (base.Initialize() == false) return false;

        return true;
    }

    public override void SetInfo(string key) {
        base.SetInfo(key);

        Level = 1;
        Exp = 0;
    }

    #endregion


    /// <summary>
    /// 해당 레벨에 도달하기 위해 필요한 경험치량
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    // TODO:: LevelData로 관리하는 방법을 찾아볼까!
    private float Temp_GetRequiredExp(int level) {
        // 최대 레벨이라면 return -1;
        // 0 레벨이라면 return 0;
        return 1;
    }

    #region InputSystem

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        Direction = moveInput;
    }

    #endregion
}