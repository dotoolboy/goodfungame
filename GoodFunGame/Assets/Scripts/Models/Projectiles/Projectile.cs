using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Thing
{

    #region Properties
    public Creature Owner { get; protected set; }

    public float Damage { get; private set; }
    public int PenetrationCount { get; private set; }
    public float Duration { get; private set; }

    #endregion

    #region Fields

    // Components.
    protected SpriteRenderer _spriter;
    protected Rigidbody2D _rigidbody;

    #endregion

    #region MonoBehaviours

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag($"Wall"))
        {
            if (this.IsValid()) Main.Object.Despawn(this);
        }
    }



    #endregion

    #region Initialize / Set

    public override bool Initialize()
    {
        base.Initialize();

        _spriter = this.GetComponent<SpriteRenderer>();
        _rigidbody = this.GetComponent<Rigidbody2D>();

        return true;
    }

    public virtual Projectile SetInfo(Creature owner, string key, float damage = -1, float scale = 1, float duration = 8)
    {
        Initialize();
        this.Owner = owner;
        this._spriter.sprite = Main.Resource.Load<Sprite>($"{key}.sprite");
        this.Damage = damage == -1 ? owner.Damage : damage;
        this.transform.localScale = Vector3.one * scale;
        this.Duration = duration;

        if (this.gameObject.activeInHierarchy) StartCoroutine(CoCheckDestroy());

        return this;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    #endregion

    private IEnumerator CoCheckDestroy()
    {
        yield return new WaitForSeconds(Duration);
        Main.Object.Despawn(this);
    }

}