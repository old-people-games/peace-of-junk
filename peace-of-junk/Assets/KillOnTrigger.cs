using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<CharacterController>();
            player.Die();
        }
    }
}
