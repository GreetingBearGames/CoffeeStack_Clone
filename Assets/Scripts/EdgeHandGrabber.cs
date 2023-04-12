using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EdgeHandGrabber : MonoBehaviour
{
    [HideInInspector] public bool _isGrabbed = false;
    private Transform _movingPart;
    [SerializeField] private MovementController movementController;
    [SerializeField] private GameObject grabParticle;

    private void Start()
    {
        _movingPart = transform.parent;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collected" && !_isGrabbed)
        {
            CupGrabber(other.gameObject);
            OffsetMover();
            StartCoroutine(PlayerSpeedChanger(0.5f, 0.2f, 0.3f));
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
        grabCup.transform.position = transform.TransformPoint(GetComponent<BoxCollider>().center);

        var grabbingHandAnimCont = GameObject.FindGameObjectWithTag("GrabbingHand");
        grabbingHandAnimCont.GetComponent<Animator>().SetBool("isGrabbing", true);
        grabCup.tag = "Collectable";

        Instantiate(grabParticle, grabbedCub.transform.position, Quaternion.identity);
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


    IEnumerator PlayerSpeedChanger(float speedRatio, float decrasingDuration, float waitDuraton)
    {
        movementController.SpeedChanger(speedRatio, decrasingDuration);

        // tekrar hızı olması gereken değere getiriyoruz.
        yield return new WaitForSeconds(waitDuraton);
        movementController.SpeedChanger(1 / speedRatio, 1f);

    }
}
