using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature
{

    #region Properties

    // Inputs.
    public Vector2 Input { get; protected set; }

    // Status.
    public float ExpMultiplier { get; protected set; }
    public float CollectDistance => 4.0f;   // TODO:: NO HARDCODING!

    // State.
    public int Level { get; private set; } = 1;
    public float Exp
    {
        get => _exp;
        set
        {
            _exp += (value - _exp) * ExpMultiplier;
            // ==================== 레벨업 처리 ====================
            int level = Level;
            while (true)
            {
                float requiredExp = Temp_GetRequiredExp(level + 1);
                if (requiredExp < 0 || _exp < requiredExp) break;
                level++;
            }
            if (level != Level)
            {
                Level = level;
                cbOnPlayerLevelUp?.Invoke();
            }
            // =====================================================

            cbOnPlayerDataUpdated?.Invoke();
        }
    }
    public float ExpRatio
    {
        get
        {
            float requiredExp = Temp_GetRequiredExp(Level + 1);
            if (requiredExp < 0) return 0;
            float currentTotalExp = Temp_GetRequiredExp(Level);
            return (Exp - currentTotalExp) / (requiredExp - currentTotalExp);
        }
    }
    public int KillCount
    {
        get => _killCount;
        set
        {
            _killCount = value;
            cbOnPlayerDataUpdated?.Invoke();
        }
    }
    public int ScoreCount
    {
        get => _scoreCount;
        set
        {
            _scoreCount = value;
            cbOnPlayerDataUpdated?.Invoke();
        }
    }

    public int GoldCount
    {
        get => _goldCount;
        set
        {
            _goldCount = value;
            cbOnPlayerDataUpdated?.Invoke();
        }
    }

    #endregion

    #region Fields

    // State.
    private float _exp;
    private int _killCount;
    private int _scoreCount;
    private int _goldCount;
    [SerializeField] private float _speed;
    [SerializeField] private float _invincibilityTime = 3f;  // 무적 시간

    // Callbacks.
    public Action cbOnPlayerLevelUp;
    public Action cbOnPlayerDataUpdated;
    public delegate void PlayerHealthChanged();
    public event PlayerHealthChanged OnPlayerHealthChanged;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Enemy")
        {
            OnHit(collision.gameObject);

            // 무적
            _collider.enabled = false;
            StartCoroutine(EnableColliderAfterInvincibility());

            // 알파값 변경
            StartCoroutine(AlphaModifyAfterCollision());

            // HeartUI
            OnPlayerHealthChanged?.Invoke();
        }
    }

    #endregion

    #region Initialize / Set

    public override bool Initialize()
    {
        if (base.Initialize() == false) return false;
        SetStatus(true);
        return true;
    }

    public override void SetInfo(string key)
    {
        base.SetInfo(key);

        Level = 1;
        Exp = 0;
    }

    #endregion

    #region State
    protected override void OnStateEntered_Dead()
    {
        base.OnStateEntered_Dead();

        // TODO:: 오브젝트 디스폰
        //Main.Resource.Destroy(gameObject);

        // 게임 오버 화면 띄우기
        Main.UI.ShowPopupUI<UI_Popup_GameOver>().SetInfo();
    }
    #endregion

    /// <summary>
    /// 해당 레벨에 도달하기 위해 필요한 경험치량
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    // TODO:: LevelData로 관리하는 방법을 찾아볼까!
    private float Temp_GetRequiredExp(int level)
    {
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

    #region Coroutine

    IEnumerator EnableColliderAfterInvincibility()
    {
        yield return new WaitForSeconds(_invincibilityTime);
        _collider.enabled = true;
    }

    IEnumerator AlphaModifyAfterCollision()
    {
        float targetAlpha = 0.1f;

        Color startColor = _spriter.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        for (int i = 0; i < 3; ++i)
        {
            yield return FadeColor(startColor, targetColor, _invincibilityTime / 6);
            yield return FadeColor(targetColor, startColor, _invincibilityTime / 6);
        }

        _spriter.color = startColor;
    }
    
    IEnumerator FadeColor(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            _spriter.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    #endregion
}