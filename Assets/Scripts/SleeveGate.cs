using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SleeveGate : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private void Rotate()
    {
        canvas.GetComponent<RectTransform>().DORotate(Vector3.up * 90, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collected" || other.tag == "Player")
        {
            Rotate();
        }
    }
}
