using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PG_Fan : ProjectileGenerator
{
    public float StartAngle { get; protected set; }
    public float SpreadAngle { get; protected set; }
    public override bool CanShot => Count > 1;

    public void Initialize(Creature owner, string key, int count, float time, float speed, float startAngle = 0, float spreadAngle = 60)
    {
        base.Initialize(owner, key, count, time, speed);
        this.StartAngle = startAngle;
        this.SpreadAngle = spreadAngle;
    }

    protected override IEnumerator CoShot()
    {
        float startAngle = StartAngle * Mathf.Deg2Rad;
        float endAngle = (StartAngle + SpreadAngle) * Mathf.Deg2Rad;
        float deltaAngle = (endAngle - startAngle) / (Count - 1);
        float deltaTime = Time / Count;
        for (int i = 0; i < Count; i++)
        {
            float x = Mathf.Cos(startAngle + i * deltaAngle);
            float y = Mathf.Sin(startAngle + i * deltaAngle);
            Vector2 direction = new Vector2(x, y).normalized;

            GenerateProjectile().SetVelocity(direction * Speed);

            if (deltaTime > 0) yield return new WaitForSeconds(deltaTime);
        }
        yield break;
    }
}