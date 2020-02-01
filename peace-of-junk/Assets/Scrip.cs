using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Scrip : MonoBehaviour
{

    public float Lifetime;
    private DateTime timeToDie;
    private void OnTriggerEnter2D(Collider2D other)
    {
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
    }

    private void Start()
    {
        timeToDie = DateTime.UtcNow.AddSeconds(Lifetime);
    }

    private void Update()
    {
        if (DateTime.UtcNow > timeToDie)
        {
            Destroy(gameObject);
        }
    }
}