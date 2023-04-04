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
            StartCoroutine(FinishMoneyUISpawner(other.transform.position));
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


    IEnumerator FinishMoneyUISpawner(Vector3 cupPosition)
    {
        _gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");

        for (int i = 0; i < 4; i++)
        {
            var obj = Instantiate(_finishMoneyPrefab);
            obj.transform.SetParent(_gameCanvas.transform, false);
            WorldtoUIPosition(cupPosition, obj, true);
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


    private void WorldtoUIPosition(Vector3 worldPosition, GameObject uiObject, bool isRandomness)
    {
        var canvasRect = _gameCanvas.GetComponent<RectTransform>();
        var viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        var WorldObject_ScreenPosition = new Vector2(
                                        ((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                                        ((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        if (isRandomness)
        {
            WorldObject_ScreenPosition = new Vector2(WorldObject_ScreenPosition.x + Random.Range(-100f, 100f),
                                            WorldObject_ScreenPosition.y + Random.Range(-100f, 100f));
        }

        uiObject.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }


}
