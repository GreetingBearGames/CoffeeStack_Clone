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

            if (current > 3.7) //Road Edges
                current = 3.7f;
            else if(current <-3.7)
                current = -3.7f;

            position = new Vector3(current,position.y,position.z);  
            player.position = position;
        }
    }
    public void SpeedChanger(float speedRatio, float duration)
    {
        var newSpeedValue = movementSpeed * speedRatio;
        // Tween a float called forwardSpeed to newSpeedValue in duration second
        DOTween.To(() => movementSpeed, x => movementSpeed = x, newSpeedValue, duration);
    }
}
