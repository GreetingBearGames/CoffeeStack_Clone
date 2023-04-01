using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour, IDragHandler
{
    [SerializeField] Transform player;
    [SerializeField] float movementSpeed;
    [SerializeField] float horizontalSpeed;

    float horizontal;

    void Update()
    {
        player.transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime)); // Forward movement
    }

    public void OnDrag(PointerEventData eventData) // Slide Movement
    {
        Vector3 position = player.position;
        float current = position.x;
        current += eventData.delta.x * horizontalSpeed;

        if (current > 4.5) //Road Edges
            current = 4.5f;
        else if(current <-4.5)
            current = -4.5f;

        position = new Vector3(current,position.y,position.z);  
        player.position = position;
    }   
}