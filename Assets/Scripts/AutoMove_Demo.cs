using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoMove_Demo : MonoBehaviour
{
    [SerializeField] float forwardSpeed, horizontalSpeed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, forwardSpeed * Time.deltaTime);
        if (Input.GetKey("left") || Input.GetKey("right"))
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * horizontalSpeed, 0, 0);
        }

    }

    public void SpeedChanger(float speedRatio, float duration)
    {
        var newSpeedValue = forwardSpeed * speedRatio;
        // Tween a float called forwardSpeed to newSpeedValue in duration second
        DOTween.To(() => forwardSpeed, x => forwardSpeed = x, newSpeedValue, duration);
    }
}
