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

    public Animator animator;
    private CapsuleCollider2D _collider;
    private Collision2D _platformCollision;
    private Collision2D _previousCollision;

    public Transform CameraTarget;

    public GameObject[] bodyParts;

    public int deathTimer;

    private Coroutine _deathRoutine;
    private Coroutine _platformRoutine;

    public AudioSource audioSource;
    [Header("AudioClips")] public AudioClip jumpSound;
    public AudioClip deathSound;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        cameraController camera = FindObjectOfType<cameraController>();
        camera.AssignCamera(CameraTarget);
    }

    public void Die()
    {
        _collider.enabled = false;
        audioSource.clip = deathSound;
        audioSource.Play();
        foreach (var part in bodyParts)
        {
            part.transform.parent = null;
            var rigidbody = part.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            var collider = part.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
            Destroy(part, 2f);
        }

        cameraController camera = FindObjectOfType<cameraController>();
        camera.RemoveCameraTarget();
        _deathRoutine = StartCoroutine(DeathRoutine());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_platformCollision == null)
        {
            _platformCollision = other;
        }

        _previousCollision = _platformCollision;
        _platformCollision = other;
        animator.SetBool("Jumping", false);
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathTimer);
        SpawnPoint spawn = FindObjectOfType<SpawnPoint>();
        if (spawn != null) spawn.SpawnPlayer();
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }

    private IEnumerator FallThroughPlatform(Collider2D collider)
    {
        collider.isTrigger = true;
        yield return new WaitForSeconds(0.2f);
        collider.isTrigger = false;
        _platformRoutine = null;
    }

    void FixedUpdate()
    {
        if (_platformCollision == null)
        {
            return;
        }


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        animator.SetFloat("WalkSpeed", 0);
        if (_collider.IsTouching(_platformCollision.collider) && moveHorizontal < -0.3f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("WalkSpeed", -moveHorizontal);
        }

        if (_collider.IsTouching(_platformCollision.collider) && moveHorizontal > 0.3f)
        {
            animator.SetFloat("WalkSpeed", moveHorizontal);
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (moveVertical < -0.4f && Input.GetButton("Jump") && _platformRoutine == null &&
            !_platformCollision.collider.CompareTag("Ground"))
        {
            _platformRoutine = StartCoroutine(FallThroughPlatform(_platformCollision.collider));
        }


        if ((_platformCollision != null &&
             ((_collider.IsTouching(_platformCollision.collider) ||
               _collider.IsTouching(_previousCollision.collider)) && Input.GetButtonUp("Jump") && _jumpForce > 0)))
        {
            rb2d.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            animator.SetBool("BeforeJump", false);
            animator.SetBool("Jumping", true);
            audioSource.clip = jumpSound;
            audioSource.Play();
            _jumpForce = 0;
        }

        if (Input.GetButton("Jump") && _jumpForce <= maxJumpAmount)
        {
            animator.SetBool("BeforeJump", true);
            _jumpForce += jumpAddedByHolding;
        }

        if (Input.GetButton("Jump"))
        {
            return;
        }

        rb2d.velocity = new Vector2(moveHorizontal * MaxSpeed, rb2d.velocity.y);
    }
}