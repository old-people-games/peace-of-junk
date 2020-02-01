using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireTrap : MonoBehaviour
{
    public Scrip Tire;
    public float AndgularSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Tire.Activate(AndgularSpeed);
        }
    }
}