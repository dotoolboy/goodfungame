using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectShieldSkill : MonoBehaviour
{


    public void Activate()
    {
        StartCoroutine(ReflectShieldCoroutine());
    }

    private IEnumerator ReflectShieldCoroutine()
    {
        // 반사 실드 생성
        GameObject newObj = Main.Resource.InstantiatePrefab("ReflectShield.prefab");

        // 일정 시간 동안 실드 유지
        yield return new WaitForSeconds(5f); // 예시로 5초 동안 유지

        // 실드 제거
        Main.Resource.Destroy(newObj);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            // 반사된 탄막의 위치에서 플레이어 탄막을 생성
            Projectile projectile = Main.Object.Spawn<Projectile>("", this.transform.position);

            // 반사된 탄막의 방향을 반대로 설정
            Vector2 reflectedDirection = -other.GetComponent<Rigidbody2D>().velocity.normalized;

            // 플레이어 탄막에 반사된 방향 설정
            projectile.GetComponent<Rigidbody2D>().velocity = reflectedDirection; 

        }
    }
}
