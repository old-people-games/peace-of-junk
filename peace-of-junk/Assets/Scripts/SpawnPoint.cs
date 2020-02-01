using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject player;

    public Transform spawnPoint;
   public void SpawnPlayer()
   {
       Instantiate(player, spawnPoint.position, spawnPoint.rotation);
   }
}
