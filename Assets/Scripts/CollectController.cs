using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectController : MonoBehaviour
{

    public Transform lastOne;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject other)
    {
        lastOne = NodeManager.Instance.lastNode;
        other.transform.position = lastOne.transform.position + Vector3.forward;
        other.AddComponent<NodeController>().connectedNode = lastOne.transform;
        other.AddComponent<CollectController>();

        NodeManager.Instance.lastNode = other.gameObject.transform;
        NodeManager.Instance.nodes.Add(other);

        other.tag = "Collected";
        other.GetComponent<BoxCollider>().isTrigger = false;
        other.GetComponent<NodeController>().StartScaleAnimation();
    }



}