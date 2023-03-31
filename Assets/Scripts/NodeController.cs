using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NodeController : MonoBehaviour
{
    [SerializeField] public Transform connectedNode;
    [SerializeField] public float movementSpeed=10;
    
    void Update()
    {
		try
		{
            transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, connectedNode.position.x, Time.deltaTime*movementSpeed),
                transform.position.y,
                connectedNode.position.z + 1);
        }
        catch (System.Exception)
		{
            return;
			throw;
		}  
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
}