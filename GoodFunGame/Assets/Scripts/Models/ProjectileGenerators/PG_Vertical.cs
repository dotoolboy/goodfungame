using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PG_Vertical : ProjectileGenerator
{
    public float ShotInterval = 0.5f; // 발사 간격

    public void Initialize(Creature owner, string key, int count, float time, float speed, float startAngle = 0)
    {
        base.Initialize(owner, key, count, time, speed);
    }
    protected override IEnumerator CoShot()
    {
        float deltaTime = Time / Count;

        for (int i = 0; i < Count; i++)
        {
            // 아래쪽 방향으로 발사
            Vector2 direction = Vector2.down;

            // 5발을 ShotInterval 간격으로 발사
            for (int j = 0; j < Count; j++)
            {
                GenerateProjectile().SetVelocity(direction * Speed);

                // 발사 간격 대기
                if (j < Count - 1)
                {
                    yield return new WaitForSeconds(ShotInterval);
                }
            }

            // 다음 발사 대기 시간
            if (deltaTime > 0 && i < Count - 1)
            {
                yield return new WaitForSeconds(deltaTime);
            }
        }

        yield break;
    }

}
