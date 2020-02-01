using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrip : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            var cController = other.GetComponent<CharacterController>();

            if(cController == null){
                return;
            }

            cController.Die();
        }
    }
}
