using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeHandController : MonoBehaviour
{
    private float _savedTime = 0;
    float _delay = 0.5f;


    private void OnTriggerStay(Collider other)
    {
        if ((Time.time - _savedTime) > _delay)
        {
            _savedTime = Time.time;

            if (other.tag == "Hand")
            {
                MoveEdgeHand(other.gameObject);
            }
        }
    }

    private void MoveEdgeHand(GameObject triggeredObj)
    {
        var movingPart = transform.GetChild(0);
        var newXPos = triggeredObj.transform.position.x;
        movingPart.transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);
    }
}
