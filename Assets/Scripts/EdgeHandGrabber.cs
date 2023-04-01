using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EdgeHandGrabber : MonoBehaviour
{
    public bool _isGrabbed = false;
    private Transform _movingPart;
    private AutoMove_Demo _autoMove_Demo;

    private void Start()
    {
        _movingPart = transform.parent;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collected" && !_isGrabbed)
        {
            Debug.Log("collided " + other.gameObject.name);
            OffsetMover();
            CupGrabber(other.gameObject);
            StartCoroutine(PlayerSpeedChanger(0.25f, 0.2f, 0.5f));
            _isGrabbed = true;
        }
    }


    private void OffsetMover()
    {
        var offset = 2f;
        var offsetXPos = _movingPart.position.x + offset;
        _movingPart.DOMoveX(offsetXPos, 0.5f);
    }


    private void CupGrabber(GameObject grabbedCub)
    {
        grabbedCub.transform.parent = _movingPart;
    }


    IEnumerator PlayerSpeedChanger(float speedRatio, float decrasingDuration, float waitDuraton)
    {
        _autoMove_Demo = GameObject.FindGameObjectWithTag("Player").GetComponent<AutoMove_Demo>();
        _autoMove_Demo.SpeedChanger(speedRatio, decrasingDuration);

        // tekrar hızı olması gereken değere getiriyoruz.
        yield return new WaitForSeconds(waitDuraton);
        _autoMove_Demo.SpeedChanger(1 / speedRatio, 1f);

    }
}
