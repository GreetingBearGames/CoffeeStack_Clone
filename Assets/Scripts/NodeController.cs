using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NodeController : MonoBehaviour
{
    [SerializeField] ValueController valueController;
    [SerializeField] public Transform connectedNode;
    [SerializeField] public float movementSpeed=15;
    public int glassValue=2;
    

    private void Start() {
        valueController = NodeManager.Instance.valueController;
        valueController.value += 2;
    }
    void Update()
    {
		try
		{

            if (connectedNode.CompareTag("Player"))
            {
                transform.position = connectedNode.transform.position;
            }else
            {
                transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, connectedNode.position.x, Time.deltaTime*movementSpeed),
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
        if (other.CompareTag("FinishLine"))
        {
           NodeManager.Instance.MovePlayerToMid();
        }
        ValueChange(other);
    }    

    public void StartScaleAnimation(){
        StartCoroutine(MakeNodeBigger());
    }

    private IEnumerator MakeNodeBigger(){
        Vector3 scale = Vector3.one;
        scale*=1.3f;
        transform.DOScale(scale,0.1f).OnComplete(()=>{
            transform.DOScale(Vector3.one,0.1f);
        });
        
        yield return new WaitForSeconds(0.05f);

        if (!connectedNode.CompareTag("Player"))
        {
             connectedNode.GetComponent<NodeController>().StartScaleAnimation();
        }
    }

    private void ValueChange(Collider other){

        switch (other.tag)
        {
            case "Collectable":
                //Start metodunda hesaplandı.
                break;
            //Collide durumları ve değerleri için caseler burada olacak
            

            
            default:
                break;
        }
    }
}