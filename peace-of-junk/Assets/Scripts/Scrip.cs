using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scrip : MonoBehaviour
{
    public float Lifetime;
    private DateTime timeToDie;
    public GameObject particleEffect;
    public float rotation;
    public Rigidbody2D rigidbody;
    public bool DestroyOnFloorTouch;
    public bool ActivateOnTigger;
    private Rigidbody2D _rigidbody2D;
    private Rigidbody2D _rigidbody2D1;
    private Rigidbody2D _rigidbody2D2;
    private Rigidbody2D _rigidbody2D3;
    private Rigidbody2D _rigidbody2D4;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (DestroyOnFloorTouch && other != null && !other.CompareTag("Platform"))
        {
            destroyObject();
            return;
        }

        if (other != null && !other.CompareTag("Player"))
        {
            return;
        }

        CharacterController cController = other.GetComponent<CharacterController>();

        if (cController == null)
        {
            return;
        }

        cController.Die();
        destroyObject();
    }

    public void Activate(float AngularSpeed)
    {
        timeToDie = DateTime.Now.AddSeconds(Lifetime);
        ActivateOnTigger = true;
        rigidbody.AddForce(new Vector2(AngularSpeed, 0f));
    }


    private void Start()
    {
        if (!ActivateOnTigger)
        {
            timeToDie = DateTime.UtcNow.AddSeconds(Lifetime);
        }
    }

    private void Update()
    {
        if (!ActivateOnTigger && DateTime.UtcNow > timeToDie)
        {
            destroyObject();
        }

        if (rotation == 0) return;
        if (rigidbody != null) rigidbody.MoveRotation(rigidbody.rotation + rotation * Time.deltaTime);
    }
    
   

    public void destroyObject()
    {
        if (particleEffect != null)
        {
            Instantiate(particleEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}