using System;
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
    private BoxCollider2D collider;
    private Collision2D _platformCollision;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _platformCollision = other;
    }

    Vector3 velocity;
    float accelerationScalar;
    float maxSpeed;
    float friction;


    void FixedUpdate()
    {
        if (_platformCollision == null)
        {
            return;
        }


        float moveHorizontal = Input.GetAxis("Horizontal");

        transform.localScale = moveHorizontal < 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);


        if (collider.IsTouching(_platformCollision.collider) && Input.GetButtonUp("Jump") && _jumpForce > 0)
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