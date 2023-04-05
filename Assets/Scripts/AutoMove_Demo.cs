using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoMove_Demo : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 3, _horizontalSpeed = 0.03f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, _forwardSpeed * Time.deltaTime);
        if (Input.GetKey("left") || Input.GetKey("right"))
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * _horizontalSpeed, 0, 0);
        }

    }

    public void SpeedChanger(float speedRatio, float duration)
    {
        var newSpeedValue = _forwardSpeed * speedRatio;
        // Tween a float called forwardSpeed to newSpeedValue in duration second
        DOTween.To(() => _forwardSpeed, x => _forwardSpeed = x, newSpeedValue, duration);
    }
}
