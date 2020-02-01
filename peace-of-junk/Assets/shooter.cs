using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class shooter : MonoBehaviour
{
    public GameObject thingToSpawn;
    private DateTime timeKeeper;
    public float spawnInterval;
    public float velocity;

    void Start()
    {

        timeKeeper = DateTime.UtcNow.AddSeconds(spawnInterval);
    }
    void Update()
    {
        if(thingToSpawn == null){
            return; 
        }

        if(DateTime.UtcNow > timeKeeper){
            var instantiatedObject = Instantiate(thingToSpawn, transform.position, transform.rotation);
            var objectRigidbody = instantiatedObject.GetComponent<Rigidbody2D>();

            if(objectRigidbody == null){
                objectRigidbody = instantiatedObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            }

            if(objectRigidbody == null){
                return;
            }

            objectRigidbody.AddForce(transform.right * velocity);
            timeKeeper = DateTime.UtcNow.AddSeconds(spawnInterval);
        }
    }
}
