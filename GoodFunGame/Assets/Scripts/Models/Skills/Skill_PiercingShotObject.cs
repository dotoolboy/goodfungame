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
        StartCoroutine(DespawnPiercingShotObject());
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = Vector2.up * _speed * Time.fixedDeltaTime;
    }

    IEnumerator DespawnPiercingShotObject()
    {
        yield return new WaitForSeconds(10f);
    }
}
