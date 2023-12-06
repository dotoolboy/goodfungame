using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PG_Ring : ProjectileGenerator
{
    public float StartAngle { get; protected set; }

    public void Initialize(Creature owner, string key, int count, float time, float speed, float startAngle = 0)
    {
        base.Initialize(owner, key, count, time, speed);
        this.StartAngle = startAngle;
    }

    protected override IEnumerator CoShot()
    {
        float deltaAngle = 2 * Mathf.PI / Count; // 360도를 Count 개수로 등분

        for (int i = 0; i < Count; i++)
        {
            float currentAngle = i * deltaAngle + StartAngle;
            float x = Mathf.Cos(currentAngle);
            float y = Mathf.Sin(currentAngle);
            Vector2 direction = new Vector2(x, y).normalized;

            GenerateProjectile().SetVelocity(direction * Speed);

        }

        yield break;
    }
}
