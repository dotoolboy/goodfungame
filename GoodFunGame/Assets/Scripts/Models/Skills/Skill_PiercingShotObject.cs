using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PiercingShotObject : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    float _speed = 300f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyPiercingShotObject());
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = Vector2.up * _speed * Time.fixedDeltaTime;
    }

    IEnumerator DestroyPiercingShotObject()
    {
        yield return new WaitForSeconds(5f);
        Main.Resource.Destroy(gameObject);
    }
}
