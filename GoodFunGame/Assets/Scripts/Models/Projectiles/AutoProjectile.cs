using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoProjectile : MonoBehaviour
{
    GameObject _target;
    Rigidbody2D _rigidbody;

    private float _speed = 200f;
    private float _rotationSpeed = 5f;
    bool _isTarget = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyAutoProjectile());
        SetTarget();
    }

    void Update()
    {
        if (!_isTarget || (_target != null && !_target.activeSelf))
        {
            _isTarget = false;
            SetTarget();
        }
    }

    private void FixedUpdate()
    {
        if (_isTarget)
        {
            Vector2 direction = (_target.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            
            transform.rotation
                = Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.fixedDeltaTime);

            _rigidbody.velocity = transform.up * _speed * Time.fixedDeltaTime;
        }
    }
    private void SetTarget()
    {
        List<Enemy> enemyList = Main.Object.Enemies;
        float minDistance = float.MaxValue;
        for (int i = 0; i < enemyList.Count; ++i)
        {
            if (enemyList[i].enabled == true)
            {
                float distance = Vector3.Distance(transform.position, enemyList[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    _target = enemyList[i].gameObject;
                    _isTarget = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            StopCoroutine(DestroyAutoProjectile());
            Main.Resource.Destroy(gameObject);
        }
    }

    IEnumerator DestroyAutoProjectile()
    {
        yield return new WaitForSeconds(10f);
        Main.Resource.Destroy(gameObject);
    }

}
