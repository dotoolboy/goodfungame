using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                if (_coInvincible != null) StopCoroutine(_coInvincible);
                _coInvincible = StartCoroutine(InvincibleTimer(_invincibilityTime));
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
    public bool IsPiercingAttack { get; set; } = false;
    // Skills.
    private List<SkillBase> _skills = new();

    // Callbacks.
    public Action cbOnPlayerLevelUp;
    public Action cbOnPlayerDataUpdated;
    public Action OnPlayerPiersingAttack;

    public delegate void PlayerHealthChanged();
    public event PlayerHealthChanged OnPlayerHealthChanged;

    // Coroutines.
    private Coroutine _coInvincible;
    #endregion

    #region MonoBehaviours

    private void Start()
    {
        MoveSpeed = _speed;
    }

    protected override void FixedUpdate()
    {
        _rigidbody.velocity = Direction * MoveSpeed * Time.fixedDeltaTime;

        if (_attackCooldownTimer <= 0 && !IsPiercingAttack)
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
        SetSkills();
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

    private void SetSkills()
    {
        foreach (string key in Main.Game.EquippedSkills)
        {
            Debug.Log(key);
            switch (key)
            {
                case "REFLECTSHIELD":
                    _skills.Add(this.AddComponent<Skill_ReflectShield>());
                    break;
                case "TIMEWARP":
                    _skills.Add(this.AddComponent<Skill_TimeWarp>());
                    break;
                case "ENERGYBURST":
                    _skills.Add(this.AddComponent<Skill_EnergyBurst>());
                    break;
                case "GRAVITYFIELD":
                    _skills.Add(this.AddComponent<Skill_GravityField>());
                    break;
                case "PIERCINGSHOT":
                    _skills.Add(this.AddComponent<Skill_PiercingShot>());
                    break;
                case "AUTOTARGET":
                    _skills.Add(this.AddComponent<Skill_AutoTarget>());
                    break;
                case "HOMINGMISSILE":
                    _skills.Add(this.AddComponent<Skill_HomigMissile>());
                    break;
            }
        }
        for (int i = 0; i < _skills.Count; i++)
        {
            _skills[i].Initialize();
        }
    }

    #endregion

    #region State
    protected override void OnStateEntered_Dead()
    {
        base.OnStateEntered_Dead();

        // 게임 오버 화면 띄우기
        Main.Stage.GameOver();
        Main.UI.ShowPopupUI<UI_Popup_GameOver>().SetInfo();



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


    // (InputValue value) value값을 어떤 키를 눌러서 들어왔는지 구분하는 방법을 못찾아서 임시로 z x c  나눠놨습니다
    public void OnZSkill(InputValue value)
    {
        if (!value.isPressed) return;  // 눌렀을때 true 땔때 false 호출

        if (Main.Game.EquippedSkills.Count > 0)
            _skills[0].Activate();
    }
    public void OnXSkill(InputValue value)
    {
        if (!value.isPressed) return;
        if (Main.Game.EquippedSkills.Count > 1)
            _skills[1].Activate();
    }
    public void OnCSkill(InputValue value)
    {
        if (!value.isPressed) return;
        if (Main.Game.EquippedSkills.Count > 2)
            _skills[2].Activate();
    }


    #endregion

    #region Attack

    private void Attack()
    {
        Projectile projectile = Main.Object.Spawn<Projectile>("", this.transform.position);
        projectile.SetInfo(this, "PlayerProjectile", Damage, 1);
        projectile.SetVelocity(Vector2.up * _projectileSpeed);
        projectile.gameObject.tag = "PlayerProjectile";
    }

    #endregion

    #region Coroutine
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
    //private ReflectShieldSkill reflectShieldSkill;
    //private GravityFieldSkill gravityFieldSkill;

    // 임시 스킬 테스트

    /*
    protected override void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
        {
        //  _skills[0].Activate();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.X))
        {
          //  _skills[1].Activate();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.C))
        {
          //  _skills[2].Activate();
        }

    }
    
     */
}