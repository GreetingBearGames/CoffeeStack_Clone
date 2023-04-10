using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishMoneyUIMove : MonoBehaviour
{

    void Start()
    {
        Invoke("MovetoPosition", 0.2f);
    }

    private void MovetoPosition()
    {
        var targetPos = GameObject.FindGameObjectWithTag("FinishMoneyTarget").transform.GetComponent<RectTransform>().localPosition;
        this.transform.GetComponent<RectTransform>().DOAnchorPos(targetPos, 1f);
        Destroy(this.gameObject, 1.1f);
    }
}
