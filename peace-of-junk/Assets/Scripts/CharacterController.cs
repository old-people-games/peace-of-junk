﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float MaxSpeed;
    public float jumpAddedByHolding;
    public float maxJumpAmount;
    private float _jumpForce;
    private CapsuleCollider2D _collider;
    private Collision2D _platformCollision;

    public GameObject[] bodyParts;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    public void Die()
    {
        foreach (var part in bodyParts)
        {
            var rigidbody = part.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            var collider = part.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _platformCollision = other;
    }

    void Update()
    {
        if (_platformCollision == null)
        {
            return;
        }


        float moveHorizontal = Input.GetAxis("Horizontal");

        if (_collider.IsTouching(_platformCollision.collider) && moveHorizontal < -0.2f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (_collider.IsTouching(_platformCollision.collider) && moveHorizontal > 0.2f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        if (_collider.IsTouching(_platformCollision.collider) && Input.GetButtonUp("Jump") && _jumpForce > 0)
        {
            rb2d.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            _jumpForce = 0;
        }

        if (Input.GetButton("Jump") && _jumpForce <= maxJumpAmount)
        {
            _jumpForce += jumpAddedByHolding;
        }

        if (Input.GetButton("Jump"))
        {
            return;
        }

        rb2d.velocity = new Vector2(moveHorizontal * MaxSpeed, rb2d.velocity.y);
    }
}