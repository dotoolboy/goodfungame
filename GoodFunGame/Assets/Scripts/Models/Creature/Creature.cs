using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureState {
    IDLE,
    DEAD,
}

public class Creature : Thing {

    #region Properties

    // Input.
    public Vector2 Direction { get; protected set; } = Vector2.zero;

    // Data.
    public CreatureData Data { get; private set; }

    // Status.
    public float HpMax { get; protected set; }
    public float Damage { get; protected set; }
    public float MoveSpeed { get; protected set; }

    // State.
    public CreatureState State {
        get => _state;
        set {
            _state = value;
            switch (value) {
                case CreatureState.IDLE: OnStateEntered_Idle(); break;
                case CreatureState.DEAD: OnStateEntered_Dead(); break;
            }
        }
    }
    public float Hp {
        get => _hp;
        set {
            if (value > HpMax) _hp = HpMax;
            else if (value <= 0) {
                Hp = 0;
                if (State != CreatureState.DEAD)
                    State = CreatureState.DEAD;
            }
            else _hp = value;
        }
    }
    public Vector2 Velocity { get; protected set; }

    #endregion

    #region Fields

    // State.
    private CreatureState _state;
    private float _hp;

    // Components.
    protected SpriteRenderer _spriter;
    protected Collider2D _collider;
    protected Rigidbody2D _rigidbody;
    protected Animator _animator;

    #endregion

    #region MonoBehaviours

    protected virtual void Update() {

    }
    protected virtual void FixedUpdate() {
        Velocity = Direction.normalized * MoveSpeed;
        this.transform.Translate(Velocity * Time.fixedDeltaTime);
        _rigidbody.velocity = Vector2.zero;
    }

    #endregion

    #region Initialize / Set

    public override bool Initialize() {
        if (base.Initialize() == false) return false;

        _spriter = this.GetComponent<SpriteRenderer>();
        _collider = this.GetComponent<Collider2D>();
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();

        return true;
    }

    public virtual void SetInfo(string key) {
        Initialize();

        Data = null;    // TODO:: key를 통해 해당 크리쳐의 Data를 받아온다. ex) Main.Data.Creatures[key];

        SetStatus();
    }

    protected virtual void SetStatus(bool isFullHp = false) {
        HpMax = Data.hpMax;
        Damage = Data.damage;
        MoveSpeed = Data.moveSpeed;

        if (isFullHp) Hp = HpMax;
    }

    #endregion

    #region State

    protected virtual void OnStateEntered_Idle() { }
    protected virtual void OnStateEntered_Dead() { }

    #endregion

    public virtual void OnHit(Creature attacker, float damage) {
        Hp -= damage;
    }

}