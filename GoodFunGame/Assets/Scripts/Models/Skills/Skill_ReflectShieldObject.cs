using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ReflectShieldObject : MonoBehaviour
{

    public float Duration { get; private set; }

    void Update()
    {
        this.transform.localPosition = new(0, 0.6f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 newDirection = -rb.velocity.normalized;
            float newSpeed = rb.velocity.magnitude;

            projectile.SetInfo(Main.Object.Player, "Bullet_4_KSJ", 1);
            projectile.tag = "PlayerProjectile";
            projectile.SetVelocity(newDirection * newSpeed);
        }
    }

    public void Initialize(float duration)
    {
        this.Duration = duration;

        StartCoroutine(CoCheckDestroy());
    }

    private IEnumerator CoCheckDestroy()
    {
        yield return new WaitForSeconds(Duration);
        Main.Resource.Destroy(this.gameObject);
    }

}