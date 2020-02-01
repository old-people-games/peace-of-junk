using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class cameraController : MonoBehaviour
{
    [FormerlySerializedAs("playerTransform")] public Transform targetTransform;
    public Transform cameraTransform;
    public Vector3 cameraOffset;
    public bool focusOnPlayer;
    public float CameraSpeed;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = gameObject.transform;
        }
    }

    public void RemoveCameraTarget()
    {
        targetTransform = null;
        focusOnPlayer = false;
    }

    public void AssignCamera(Transform transform)
    {
        targetTransform = transform;
        focusOnPlayer = true;
    }

    void LateUpdate()
    {
        if (!focusOnPlayer && targetTransform == null)
        {
            return;
        }

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetTransform.position + cameraOffset,
            CameraSpeed * Time.deltaTime);
    }
}