using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour
{
    public Creature Owner { get; protected set; }
    public string Key { get; protected set; }
    public int Count { get; protected set; }
    public float Speed { get; protected set; }
    public float Time { get; protected set; }
    public virtual bool CanShot => Count > 0;

    protected Coroutine _coGenerate;

    protected virtual void Update()
    {

    }
    protected virtual void FixedUpdate()
    {

    }

    public virtual void Initialize(Creature owner, string key, int count, float time, float speed)
    {
        this.Owner = owner;
        this.Key = key;
        this.Count = count;
        this.Time = time;
        this.Speed = speed;
    }

    public void Shot()
    {
        if (!CanShot) return;
        _coGenerate ??= StartCoroutine(CoGenerate());
    }

    private IEnumerator CoGenerate()
    {
        yield return StartCoroutine(CoShot());
        Main.Object.DespawnProjectileGenerator(this);
    }

    protected virtual IEnumerator CoShot()
    {
        yield return null;
    }

    protected Projectile GenerateProjectile()
    {
        Projectile projectile = Main.Object.Spawn<Projectile>(Key, this.transform.position);
        projectile.SetInfo(Owner, Key, 10, 1, 10);
        projectile.tag = "EnemyProjectile";
        return projectile;
    }

}