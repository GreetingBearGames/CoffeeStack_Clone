using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MovementController : MonoBehaviour, IDragHandler
{
    [SerializeField] Transform player;
    [SerializeField] float movementSpeed;
    [SerializeField] float horizontalSpeed;
    float horizontal;
    public bool isFinished;

    void Update()
    {
        player.transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime)); // Forward movement
        if (isFinished)
        {
            player.transform.DOMoveX(0,1);
        }        
    }

    public void OnDrag(PointerEventData eventData) // Slide Movement
    {
        if (!isFinished)
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
}