using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    enum stateIdentifier
    {
        idle = 0,
        Walking = 1,
        Jumping = 2,
        falling = 3,
    }

    public float speed;
    private stateIdentifier state = stateIdentifier.idle;
    private Rigidbody2D rb2d;

    public float jumpAddedByHolding;
    public float maxJumpAmount;
    private float _jumpForce;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0.001f)
        {
            state = stateIdentifier.Walking;
        }
        else
        {
            state = stateIdentifier.idle;
        }


        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetButton("Jump") && _jumpForce <= maxJumpAmount)
        {
            _jumpForce += jumpAddedByHolding;
        }

        if (Input.GetButtonUp("Jump") && _jumpForce > 0)
        {
            rb2d.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            _jumpForce = 0;
        }

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddRelativeForce(movement * speed, ForceMode2D.Force);
    }
}