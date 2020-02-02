using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scrip : MonoBehaviour
{
    public float Lifetime;
    public GameObject particleEffect;
    public float rotation;
    public new Rigidbody2D rigidbody;
    public bool DestroyOnFloorTouch;
    public bool ActivateOnTigger;

    private void Start()
    {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        if (!ActivateOnTigger)
        {
            StartCoroutine(destroyObject());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (DestroyOnFloorTouch && other.CompareTag("Platform"))
        {
            Lifetime = 0;
            StartCoroutine(destroyObject());
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        CharacterController cController = other.GetComponent<CharacterController>();

        if (cController == null)
        {
            return;
        }

        cController.Die();
        Lifetime = 0f;
        StartCoroutine(destroyObject());
    }

    public void Activate(float AngularSpeed)
    {
        ActivateOnTigger = true;
        if (rigidbody != null) rigidbody.velocity = new Vector2(AngularSpeed, 0f);
        StartCoroutine(destroyObject());
    }


    private void Update()
    {
        if (rotation == 0) return;
        if (rigidbody != null) rigidbody.MoveRotation(rigidbody.rotation + rotation * Time.deltaTime);
    }


    public IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(Lifetime);
        if (particleEffect != null)
        {
            Instantiate(particleEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}