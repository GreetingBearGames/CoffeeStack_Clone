using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishHandGrabber : MonoBehaviour
{
    [SerializeField] private GameObject _finishMoneyPrefab;
    private GameObject _gameCanvas;
    private bool _isGrabbed = false;
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
            RemainedCupChecker(other.gameObject);
            CupGrabber(other.gameObject);
            StartCoroutine("FinishMoneyUISpawner");
            OffsetMover();
            _isGrabbed = true;
        }
    }


    private void RemainedCupChecker(GameObject grabbedCub)
    {
        var cupCount = grabbedCub.transform.parent.childCount - 1;      //o bardağı yok edeceği için 1 eksilttik.

        if (cupCount == 0)
        {
            OtherHandsOffsetMover();
        }
    }


    private void CupGrabber(GameObject grabbedCub)
    {
        grabbedCub.transform.parent = _movingPart;

        //BURDA ELİN ANİMASYONUNU OYNATACAKSIN
    }


    IEnumerator FinishMoneyUISpawner()
    {
        _gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");

        for (int i = 0; i < 4; i++)
        {
            var randomSpawnPos = new Vector3(transform.position.x + Random.Range(-100f, 100f),
                                            transform.position.y + Random.Range(-100f, 100f),
                                            transform.position.z);
            var obj = Instantiate(_finishMoneyPrefab, randomSpawnPos, Quaternion.identity);
            obj.transform.SetParent(_gameCanvas.transform, false);
            yield return new WaitForSeconds(0.15f);
        }
    }


    private void OffsetMover()
    {
        var offset = 2f;

        if (transform.parent.GetChild(1).transform.position.x < 0)      //Sol tarafta duran el mi yoksa sağ tarafta duran el mi anlamak için.
        {
            offset *= -1;
        }

        var offsetXPos = _movingPart.position.x + offset;
        _movingPart.DOMoveX(offsetXPos, 0.5f);
    }


    private void OtherHandsOffsetMover()
    {
        var allHandsParent = transform.parent.parent.parent;
        var nextChildNumber = transform.parent.parent.GetSiblingIndex() + 1;
        var childCount = allHandsParent.childCount;

        while (nextChildNumber < childCount)
        {
            var handMoveablePart = allHandsParent.GetChild(nextChildNumber).GetChild(0);

            var offset = 2f;

            if (handMoveablePart.GetChild(1).transform.position.x < 0)      //Sol tarafta duran el mi yoksa sağ tarafta duran el mi anlamak için.
            {
                offset *= -1;
            }


            var offsetXPos = handMoveablePart.position.x + offset;
            handMoveablePart.DOMoveX(offsetXPos, 0.5f);

            nextChildNumber++;
        }
    }
}
