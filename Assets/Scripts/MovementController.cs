using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    [SerializeField] float horizontalSpeed;

    float horizontal;

    void Update()
    {
        if (transform.position.x > 240.7f && transform.position.x < 249.25f) // Road 
        {
            horizontal = Input.GetAxis("Horizontal");

            transform.Translate(new Vector3(horizontal * horizontalSpeed * Time.deltaTime, 0, movementSpeed * Time.deltaTime));
        }
        else
        {
            transform.Translate(new Vector3(horizontal * -horizontalSpeed * Time.deltaTime, 0, movementSpeed * Time.deltaTime));
        }
    }
}