using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishCupSpreader : MonoBehaviour
{
    [SerializeField] private Transform[] jumpPositions = new Transform[8];
    [SerializeField] private GameObject _finishMoneyPrefab;
    [SerializeField] private float _jumpLift = 3f;
    [SerializeField] private float _jumpDuration = 0.5f;
    private bool[] isPositionSuitable = new bool[8];
    private GameObject _gameCanvas;



    private void Start()
    {
        EmptyAllPositions();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collected")
        {
            Destroy(other.GetComponent<NodeController>());
            Destroy(other.GetComponent<CollectController>());
            NodeManager.Instance.nodes.RemoveAt(NodeManager.Instance.nodes.IndexOf(other.gameObject));
            other.transform.SetParent(transform);           //el ile beraber hareket etmesin diye
            other.GetComponent<BoxCollider>().isTrigger = true;     //üstüste gelebilsinler diye
            var rb = other.transform.GetComponent<Rigidbody>();

            for (int i = 0; i < isPositionSuitable.Length; i++)
            {
                if (isPositionSuitable[i] == true)      //eğer o slot boş ise
                {
                    Vector3 jumpPos = jumpPositions[i].transform.position;
                    rb.transform.DOJump(jumpPos, _jumpLift, 1, _jumpDuration);
                    isPositionSuitable[i] = false;
                    StartCoroutine(FinishMoneyUISpawner(jumpPos));


                    if (i == isPositionSuitable.Length - 1) { EmptyAllPositions(); }        //tüm slotlar doldu ise
                    break;
                }
            }
        }
    }


    private void EmptyAllPositions()
    {
        for (int i = 0; i < isPositionSuitable.Length; i++)
        {
            isPositionSuitable[i] = true;
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
