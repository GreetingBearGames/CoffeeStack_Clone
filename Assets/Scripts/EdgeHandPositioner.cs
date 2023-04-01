using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EdgeHandPositioner : MonoBehaviour
{
    [SerializeField] EdgeHandGrabber edgeHandGrabber;
    private float _savedTime = 0;
    private float _delay = 0.3f;




    private void OnTriggerStay(Collider other)
    {
        if (edgeHandGrabber._isGrabbed == false)
        {
            if (other.tag == "Hand")
            {
                if ((Time.time - _savedTime) > _delay)
                {
                    MoveEdgeHand(other.gameObject);
                    _savedTime = Time.time;
                }
            }
        }

    }

    private void MoveEdgeHand(GameObject triggeredObj)
    {
        var newXPos = triggeredObj.transform.position.x;
        var movingObj = transform.parent.GetChild(0);
        movingObj.transform.DOMoveX(newXPos, 0.5f);
    }

}
