using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectController : MonoBehaviour
{
    public Transform lastOne;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            Collect(other.gameObject);
        }

        ShowValue(other);
    }

    private void Collect(GameObject other)
    {
        lastOne = NodeManager.Instance.lastNode;
        other.transform.position = lastOne.transform.position + Vector3.forward;
        if (NodeManager.Instance.nodes.Count == 0)
        {
            other.transform.position = transform.position;
        }
        other.AddComponent<NodeController>().connectedNode = lastOne.transform;
        other.AddComponent<CollectController>();

        NodeManager.Instance.lastNode = other.gameObject.transform;
        NodeManager.Instance.nodes.Add(other);

        other.tag = "Collected";
        other.GetComponent<BoxCollider>().isTrigger = false;
        other.GetComponent<NodeController>().StartScaleAnimation();
    }

    private void ShowValue(Collider other){
        GameObject valueText = Instantiate(NodeManager.Instance.showValuePrefab,
                                        other.transform.position,
                                        other.transform.rotation,
                                        NodeManager.Instance.Player);
        Destroy(valueText,1f);
        switch (tag)
        {
            case "LidDoor":
                valueText.GetComponentInChildren<TextMeshProUGUI>().text = "$5";
                break;
            case "Collectable":
                valueText.GetComponentInChildren<TextMeshProUGUI>().text = other.GetComponent<NodeController>().glassValue.ToString();
                break;
            default:
                break;
        }

    }

}