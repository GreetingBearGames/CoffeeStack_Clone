using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishLinePositioner : MonoBehaviour
{
    private bool _isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" && !_isTriggered)
        {
            var handParent = other.transform.parent;
            handParent.DOMoveX(0, 0.3f);
        }

        if (other.tag == "Collected" && !_isTriggered)
        {

            var handParent = other.transform.parent.parent;
            handParent.DOMoveX(0, 0.3f);
        }


        //TÜM PLAYER KONTROLÜNÜ DEVRE DIŞI BIRAK!
    }
}
