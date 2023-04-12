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
            SoundManager.instance.Play("CupCollect");
            Collect(other.gameObject);
        }
        if(!this.gameObject.CompareTag("Player")){
            if (other.CompareTag("LidMaker")) {
                SoundManager.instance.Play("CupLevelUp");
                this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
            } else if (other.CompareTag("CoffeeAdder")) {
                SoundManager.instance.Play("CoffeeFill");
                    this.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            }else if (other.CompareTag("CremaFoam")) {
                SoundManager.instance.Play("CupLevelUp");
                this.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
            }else if (other.CompareTag("SellGate")) {
                var cupIndex = NodeManager.Instance.nodes.IndexOf(this.gameObject);
                var grabCup = NodeManager.Instance.nodes[cupIndex];
                Destroy(grabCup.GetComponent<NodeController>());
                Destroy(grabCup.GetComponent<CollectController>());
                NodeManager.Instance.nodes.RemoveAt(cupIndex);
                if (NodeManager.Instance.nodes.Count == 0)
                {
                    NodeManager.Instance.lastNode = NodeManager.Instance.Player.transform;
                }
                else
                {
                    NodeManager.Instance.lastNode = NodeManager.Instance.nodes[NodeManager.Instance.nodes.Count - 1].transform;
                }
                this.transform.DOLocalMoveX(5.5f, 0.5f);
            }
            else if (other.CompareTag("SleeveAdder"))
            {
                this.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                //this.transform.DOLocalMoveY(this.transform.position.y - 0.5f, 0.2f).OnComplete(() =>{
                //this.transform.DOLocalMoveY(this.transform.position.y + 0.5f, 0.2f);
                //});
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

    private void ShowValue(GameObject other)
    {

    }

}
