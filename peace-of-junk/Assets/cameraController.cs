using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    public Transform playerTransform;
    public Transform cameraTransform;
    public Vector3 cameraOffset;
    public bool focusOnPlayer;

    void Start()
    {
        if(cameraTransform == null){
            cameraTransform = gameObject.transform;
        }
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!focusOnPlayer){
            return;
        }
        cameraTransform.position = playerTransform.position + cameraOffset;
    }
}
