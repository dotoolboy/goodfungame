using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_HomigMissileObject : MonoBehaviour
{
    float _speed = 10f;

    Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        Vector2 dir = new Vector2(Random.Range(-1f, 1f), 0.5f).normalized;
        _rigidbody.velocity = dir * _speed;

        StartCoroutine(DestroyHomingMissile());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().OnHit(Main.Object.Player);
    }


    IEnumerator DestroyHomingMissile()
    {
        yield return new WaitForSeconds(5f);
        Main.Resource.Destroy(gameObject);
    }
}
