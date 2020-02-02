using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewPickup : MonoBehaviour
{
    public GameObject ParticleEffect;
    public SpawnPoint SpawnPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Instantiate(ParticleEffect, transform.position, transform.rotation);
        SpawnPoint.SetSpawn(transform);
        Destroy(gameObject);
    }
}
