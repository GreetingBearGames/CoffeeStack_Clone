using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectController : MonoBehaviour
{
    public Transform lastOne;
    GameObject newLidGameObject, newCoffeeAddedGameObject, newMilkAddedGameObject,
    newCoffeeFoamedGameObject, newCremaAddedGameObject, newSleevedGameObject, currentGameObject;

    private void OnTriggerEnter(Collider other)
    {
        currentGameObject = this.gameObject;
        if (other.CompareTag("Collectable"))
        {
            Collect(other.gameObject);
        }
        else if (other.CompareTag("LidAdder")) {
            var currentTransform = currentGameObject.transform;
            Destroy(currentGameObject);
            Instantiate(newLidGameObject, currentTransform);
        } else if (other.CompareTag("CoffeeAdder")) {
            var currentTransform = currentGameObject.transform;
            Destroy(currentGameObject);
            Instantiate(newCoffeeAddedGameObject, currentTransform);
        } else if (other.CompareTag("CoffeeFoamer")) {
            var currentTransform = currentGameObject.transform;
            Destroy(currentGameObject);
            Instantiate(newCoffeeFoamedGameObject, currentTransform);
        } else if (other.CompareTag("MilkAdder")) {
            var currentTransform = currentGameObject.transform;
            Destroy(currentGameObject);
            Instantiate(newMilkAddedGameObject, currentTransform);
        } else if (other.CompareTag("CremaCupGate")) {
            var currentTransform = currentGameObject.transform;
            Destroy(currentGameObject);
            Instantiate(newCremaAddedGameObject, currentTransform);
        } else if (other.CompareTag("SellGate")) {
            Vector3.MoveTowards(currentGameObject.transform.position, currentGameObject.transform.position + Vector3.right, 1f);
            Destroy(currentGameObject);
        } else if (other.CompareTag("SleeveAdder")) {
            var currentTransform = currentGameObject.transform;
            Destroy(currentGameObject);
            Instantiate(newSleevedGameObject, currentTransform);
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