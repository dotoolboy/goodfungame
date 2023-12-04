using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Creature
{ 
    [SerializeField] private float _speed;

    private void Start()
    {
        //MoveSpeed = _speed;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Direction * _speed * Time.fixedDeltaTime;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        Direction = moveInput;
    }
}
