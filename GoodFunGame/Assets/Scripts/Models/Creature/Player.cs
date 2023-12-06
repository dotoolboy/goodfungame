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
    public float CollectDistance => 4.0f;   // TODO:: NO HARDCODING!

    public int KillCount
    {
        get => _killCount;
        set
        {
            _killCount = value;
            cbOnPlayerDataUpdated?.Invoke();
        }
    }

    public bool Invincible
    {
        get => _invincible; 
        set
        {
            _invincible = value;
            if (_invincible)
            {
                StartCoroutine(InvincibleTimer(_invincibilityTime));
            }
        }
    }

    private IEnumerator InvincibleTimer(float invincibilityTime)
    {
        yield return new WaitForSeconds(invincibilityTime);
        Invincible = false;
    }

    #endregion

    #region Fields

    // State.
    private float _exp;
    private int _killCount;
    private float _attackCooldown;
    private float _attackCooldownTimer;

    //private float _basicShotSpawnTime = 0.5f;
    private float _projectileSpeed = 5f;

    [SerializeField] private float _speed;
    [SerializeField] private float _invincibilityTime = 3f;  // 무적 시간
    private bool _invincible = false;

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
        //StartCoroutine(BasicShot());

        // 임시 스킬 테스트
        //timeWarpSkill = gameObject.AddComponent<TimeWarpSkill>();
        reflectShieldSkill = gameObject.AddComponent<ReflectShieldSkill>();
        //gravityFieldSkill = gameObject.AddComponent<GravityFieldSkill>();
    }

    protected override void FixedUpdate()
    {
        _rigidbody.velocity = Direction * MoveSpeed * Time.fixedDeltaTime;

        if (_attackCooldownTimer <= 0)
        {
            Attack();
            _attackCooldownTimer = _attackCooldown;
        }
        _attackCooldownTimer -= Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Invincible) return;
        if (collision.CompareTag("EnemyProjectile") || collision.CompareTag("Enemy"))
        {
            if (collision.CompareTag("EnemyProjectile"))
            {
                Projectile projectile = collision.gameObject.GetComponent<Projectile>();
                Main.Resource.Destroy(collision.gameObject);
                OnHit(projectile.Owner);
                Invincible = true;
            }
            else
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                OnHit(enemy);
                Invincible = true;
            }

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

    protected override void SetStatus(bool isFullHp = false, int MaxHp = 3)
    {
        base.SetStatus(isFullHp, MaxHp);
        _attackCooldown = 0.25f;
    }

    public override void SetInfo(string key)
    {
        base.SetInfo(key);
    }

    #endregion

    #region State
    protected override void OnStateEntered_Dead()
    {
        base.OnStateEntered_Dead();

        // 게임 오버 화면 띄우기
        Main.UI.ShowPopupUI<UI_Popup_GameOver>().SetInfo();
        //Main.UI.ShowPopupUI<UI_Popup_GameOver>();


        // TODO:: 오브젝트 디스폰
        //Main.Resource.Destroy(gameObject);
        Main.Object.Despawn<Player>(this);
    }
    #endregion

    #region InputSystem

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        Direction = moveInput;
    }

    #endregion

    #region Attack

    private void Attack()
    {
        Projectile projectile = Main.Object.Spawn<Projectile>("", this.transform.position);
        projectile.SetInfo(this, "Bullet_4_KSJ", Damage, 1);
        projectile.SetVelocity(Vector2.up * _projectileSpeed);
        projectile.gameObject.tag = "PlayerProjectile";
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

    // 임시 스킬 테스트
    //private TimeWarpSkill timeWarpSkill;
    private ReflectShieldSkill reflectShieldSkill;
    //private GravityFieldSkill gravityFieldSkill;

    // 임시 스킬 테스트
    protected override void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
        {
            reflectShieldSkill.Activate();
        }
    }
}