using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CollectController : MonoBehaviour
{
    public Transform lastOne;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            Collect(other.gameObject);
        }
        if(!this.gameObject.CompareTag("Player")){
            if (other.CompareTag("LidMaker")) {
                this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            } else if (other.CompareTag("CoffeeAdder")) {
                    this.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            }else if (other.CompareTag("CremaFoam")) {
                if(!this.transform.GetChild(0).GetChild(4).gameObject.activeInHierarchy)
                    this.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);  //texture 1
                else
                    this.transform.GetChild(0).GetChild(4).gameObject.SetActive(true); //texture 2
            }else if (other.CompareTag("SellGate")) {
                //this.transform.DOLocalMoveX(); //Local move ve remove
            }else if (other.CompareTag("SleeveAdder")) {
                this.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                other.transform.GetChild(0).DOLocalMoveX(0.0135f, 0.5f);
            }
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