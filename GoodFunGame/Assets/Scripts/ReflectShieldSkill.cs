using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectShieldSkill : MonoBehaviour
{

    private float _projectileSpeed = 5f;

    public void Activate()
    {
        StartCoroutine(ReflectShieldCoroutine());
    }

    private IEnumerator ReflectShieldCoroutine()
    {
        // 반사 실드 생성
        GameObject newObj = Main.Resource.InstantiatePrefab("ReflectShield.prefab");

        // 일정 시간 동안 실드 유지
        float elapsedTime = 0f;
        float duration = 5f;  // 예시로 5초 동안 유지


        while (elapsedTime < duration)
        {
            // 현재 위치에서 약간의 변화를 주어 위치를 변경
            Vector3 newPosition = Main.Object.Player.transform.position + new Vector3(0.0f, 0.6f, 0.0f);

            // 위치 업데이트
            newObj.transform.position = newPosition;

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        // 실드 제거
        Main.Resource.Destroy(newObj);
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            // 부딪힌 탄막의 속성 가져오기
            Rigidbody2D enemyProjectileRB = collision.GetComponent<Rigidbody2D>();
            Vector2 enemyProjectileDirection = -enemyProjectileRB.velocity.normalized;
            float enemyProjectileSpeed = enemyProjectileRB.velocity.magnitude;

            Main.Resource.Destroy(collision.gameObject);

            // 플레이어 탄막을 생성하고, 부딪힌 탄막의 방향을 반대로 설정하여 튕겨내기
            Projectile projectile = Main.Object.Spawn<Projectile>("", this.transform.position);
            projectile.SetInfo(Main.Object.Player, "Bullet_4_KSJ", 1, 1, 8);

            // 플레이어 탄막에 부딪힌 탄막의 방향을 반대로 설정하고 속도 설정
            projectile.SetVelocity(enemyProjectileDirection * enemyProjectileSpeed);
        }
        if (collision.CompareTag("EnemyProjectile"))
        {
            Main.Resource.Destroy(collision.gameObject);

            // 반사된 탄막의 위치에서 플레이어 탄막을 생성
            Projectile projectile = Main.Object.Spawn<Projectile>("", this.transform.position);
            projectile.SetInfo(Main.Object.Player, "Bullet_4_KSJ", 1, 1, 8);


            Vector2 playerDirection = transform.up;
            projectile.SetVelocity(playerDirection * _projectileSpeed);

        }
    }
}

