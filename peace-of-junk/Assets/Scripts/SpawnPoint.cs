using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject player;
    public UIManager UiManager;
    public Transform spawnPoint;
   public void SpawnPlayer()
   {
       Instantiate(player, spawnPoint.position, spawnPoint.rotation);
   }

   public void SetSpawn(Transform newSpawn)
   {
       UiManager.pickupScrew();
       spawnPoint = newSpawn;
   }
}
