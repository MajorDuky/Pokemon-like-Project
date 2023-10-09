using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerPos.position + cameraOffset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerPos.position + cameraOffset;
    }
}
