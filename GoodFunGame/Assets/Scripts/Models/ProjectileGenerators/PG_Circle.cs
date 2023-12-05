using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PG_Circle : ProjectileGenerator
{

    public float StartAngle { get; protected set; }

    public void Initialize(Creature owner, string key, int count, float time, float speed, float startAngle = 0)
    {
        base.Initialize(owner, key, count, time, speed);
        this.StartAngle = startAngle;
    }

    protected override IEnumerator CoShot()
    {
        float deltaAngle = 2 * Mathf.PI / Count;
        float deltaTime = Time / Count;
        float startAngle = StartAngle * Mathf.Deg2Rad;
        for (int i = 0; i < Count; i++)
        {
            float x = Mathf.Cos(i * deltaAngle + startAngle);
            float y = Mathf.Sin(i * deltaAngle + startAngle);
            Vector2 direction = new Vector2(x, y).normalized;

            GenerateProjectile().SetVelocity(direction * Speed);

            if (deltaTime > 0) yield return new WaitForSeconds(deltaTime);
        }
        yield break;
    }

}