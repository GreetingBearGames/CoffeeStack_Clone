using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class NodeController : MonoBehaviour
{
    [SerializeField] ValueController valueController;
    [SerializeField] public Transform connectedNode;
    [SerializeField] public float movementSpeed = 15;
    public int glassValue = 2;


    private void Start()
    {
        valueController = NodeManager.Instance.valueController;
        ShowValue(gameObject, glassValue);
    }
    void Update()
    {
        try
        {

            if (connectedNode.CompareTag("Player"))
            {
                transform.position = connectedNode.transform.position;
            }
            else
            {
                transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, connectedNode.position.x, Time.deltaTime * movementSpeed),
                transform.position.y,
                connectedNode.position.z + 1);
            }

        }
        catch (System.Exception)
        {
            return;
            throw;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ValueChange(other.gameObject);
    }

    public void StartScaleAnimation()
    {
        StartCoroutine(MakeNodeBigger());
    }

    private IEnumerator MakeNodeBigger()
    {
        Vector3 scale = Vector3.one;
        scale *= 1.3f;
        transform.DOScale(scale, 0.1f).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, 0.1f);
        });

        yield return new WaitForSeconds(0.05f);

        if (!connectedNode.CompareTag("Player"))
        {
            connectedNode.GetComponent<NodeController>().StartScaleAnimation();
        }
    }

    private void ValueChange(GameObject other)
    {
        int value = 0;

        Debug.Log(other.tag);
        switch (other.tag)
        {
            case "LidMaker":
                if (this.transform.GetChild(0).GetChild(5).gameObject.activeInHierarchy)
                    return;
                value = 1;
                ShowValue(other, value);
                break;
            case "CremaFoam":
                //Creama foam has sacond trigger               
                value = 3;
                ShowValue(other, value);
                break;
            case "CoffeeAdder":
                if (this.transform.GetChild(0).GetChild(3).gameObject.activeInHierarchy)
                    return;
                value = 3;
                ShowValue(other, value);
                break;
            case "SleeveAdder":
                if (this.transform.GetChild(0).GetChild(2).gameObject.activeInHierarchy)
                    return;
                value = 2;
                ShowValue(other, value);
                break;


            default:
                break;
        }


        glassValue += value;
    }

    public void ShowValue(GameObject other, int value)
    {
        GameObject valueText = Instantiate(NodeManager.Instance.showValuePrefab,
                                        transform.position,
                                        transform.rotation,
                                        NodeManager.Instance.Player);
        Destroy(valueText, 1f);
        valueText.GetComponentInChildren<TextMeshProUGUI>().text = "$" + value.ToString();

        GameManager.Instance.UpdateMoney(value);
        NodeManager.Instance.valueController.value += value;

    }
}
