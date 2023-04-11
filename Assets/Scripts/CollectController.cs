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
	if (other.CompareTag("FinishLine"))
        {
            NodeManager.Instance.MovePlayerToMid();
	}

    }

    private void Collect(GameObject other)
    {
        other.tag = "Collected";
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

        
        other.GetComponent<BoxCollider>().isTrigger = false;
        other.GetComponent<NodeController>().StartScaleAnimation();
    }

    private void ShowValue(GameObject other){

    }

}
