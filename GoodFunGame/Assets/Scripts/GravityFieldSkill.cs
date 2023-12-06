using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldSkill : MonoBehaviour
{
    public float fieldRadius = 7.0f; // 필드의 영향 범위
    public float attractionForce = 10.0f; // 적을 필드로 끌어당기는 힘

    // 스킬을 발동하는 메서드
    public void Activate()
    {
        // 스킬 발동 시 플레이어의 전방에 영향을 주는 처리

        
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        foreach (GameObject Projectile in enemyBullets)
        {
            // 필드 영향 범위 내에 있는지 확인
            if (Vector3.Distance(transform.position, Projectile.transform.position) <= fieldRadius)
            {
                // 필드로 끌어당기는 힘을 이용하여 적 탄막을 모으는 효과
                Vector3 forceDirection = (transform.position - Projectile.transform.position).normalized;
                Projectile.GetComponent<Rigidbody2D>().AddForce(attractionForce * forceDirection);
            }
        }
    }
}
