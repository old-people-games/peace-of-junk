using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewPickup : MonoBehaviour
{
    public GameObject ParticleEffect;
    public SpawnPoint SpawnPoint;
    public AudioSource audio;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        
        var position = transform.position;
        Instantiate(ParticleEffect, position, transform.rotation);
        GameObject spawn = Instantiate(new GameObject("spawnpoint"));
        spawn.transform.position = position;
        SpawnPoint.SetSpawn(spawn.transform);
        audio.Play();
        Destroy(gameObject, 0.7f);
    }
}
