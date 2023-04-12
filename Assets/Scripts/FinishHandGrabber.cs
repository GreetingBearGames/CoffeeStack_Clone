using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishHandGrabber : MonoBehaviour
{
    [SerializeField] private GameObject _finishMoneyPrefab;
    [SerializeField] private int moneyIncreaseConstant;
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
            CupGrabber(other.gameObject);
            RemainedCupChecker(other.gameObject);
            StartCoroutine(FinishMoneyUISpawner(other.transform.position));
            OffsetMover();
            ScoreIncreaser();
            _isGrabbed = true;
        }
    }





    private void CupGrabber(GameObject grabbedCub)
    {
        var cupIndex = NodeManager.Instance.nodes.IndexOf(grabbedCub);
        var grabCup = NodeManager.Instance.nodes[cupIndex];
        Destroy(grabCup.GetComponent<NodeController>());
        Destroy(grabCup.GetComponent<CollectController>());
        //grabCup.GetComponent<BoxCollider>().isTrigger = true;


        NodeManager.Instance.nodes.RemoveAt(cupIndex);


        if (NodeManager.Instance.nodes.Count == 0)
        {
            NodeManager.Instance.lastNode = NodeManager.Instance.Player.transform;
        }
        else
        {
            NodeManager.Instance.lastNode = NodeManager.Instance.nodes[NodeManager.Instance.nodes.Count - 1].transform;
        }

        grabCup.transform.parent = _movingPart;

        var grabbingHandAnimCont = GameObject.FindGameObjectWithTag("GrabbingHand");
        grabbingHandAnimCont.GetComponent<Animator>().SetBool("isGrabbing", true);
        //BURDA ELİN ANİMASYONUNU OYNATACAKSIN
    }

    private void RemainedCupChecker(GameObject grabbedCub)
    {
        var cupCount = NodeManager.Instance.nodes.Count;      //o bardağı yok edeceği için 1 eksilttik.
        if (cupCount == 0)
        {
            OtherHandsOffsetMover();
        }
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

        if (transform.parent.parent.transform.localScale.x == -1)      //Sol tarafta duran el mi yoksa sağ tarafta duran el mi anlamak için.
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

            if (handMoveablePart.parent.transform.localScale.x == -1)      //Sol tarafta duran el mi yoksa sağ tarafta duran el mi anlamak için.
            {
                offset *= -1;
            }


            var offsetXPos = handMoveablePart.position.x + offset;
            handMoveablePart.DOMoveX(offsetXPos, 0.5f);

            nextChildNumber++;
        }
    }

    private void ScoreIncreaser()
    {
        GameManager.Instance.UpdateMoney(moneyIncreaseConstant);
        NodeManager.Instance.valueController.value += moneyIncreaseConstant;
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
