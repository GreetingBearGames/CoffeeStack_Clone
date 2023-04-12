using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishLinePositioner : MonoBehaviour
{
    private bool _isTriggered = false;
    [SerializeField] private GameObject allFinishHands;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isTriggered)
        {
            var handParent = other.transform.parent;
            //handParent.DOMoveX(0, 0.3f);
            RemainedCupChecker(other.gameObject);
            CameraManager.Instance.SwitchCamera(CameraManager.Instance.finish1Cam);
        }

        if (other.tag == "Collected" && !_isTriggered)
        {

            var handParent = other.transform.parent.parent;
            //handParent.DOMoveX(0, 0.3f);
            CameraManager.Instance.SwitchCamera(CameraManager.Instance.finish1Cam);
        }


        //TÜM PLAYER KONTROLÜNÜ DEVRE DIŞI BIRAK!
    }


    private void RemainedCupChecker(GameObject hand)
    {
        //var cupCount = hand.transform.parent.GetChild(4).childCount;
        var cupCount = NodeManager.Instance.nodes.Count;

        if (cupCount == 0)
        {
            OtherHandsOffsetMover(hand);
        }
    }


    private void OtherHandsOffsetMover(GameObject hand)
    {
        var nextChildNumber = 0;
        var childCount = allFinishHands.transform.childCount;

        while (nextChildNumber < childCount)
        {
            var handMoveablePart = allFinishHands.transform.GetChild(nextChildNumber).GetChild(0);

            var offset = 2f;

            if (handMoveablePart.GetChild(0).transform.position.x > 0)      //Sol tarafta duran el mi yoksa sağ tarafta duran el mi anlamak için.
            {
                offset *= -1;
            }


            var offsetXPos = handMoveablePart.position.x + offset;
            handMoveablePart.DOMoveX(offsetXPos, 0.5f);

            nextChildNumber++;
        }
    }
}
