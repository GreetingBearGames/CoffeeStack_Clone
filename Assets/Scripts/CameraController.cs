using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    
    [SerializeField] float followSpeed;
    Vector3 offset;

    void Start()
    {
        offset = (transform.position) - player.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,player.position+offset,followSpeed);
    }
}