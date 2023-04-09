using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleCupThrower : MonoBehaviour
{
    [SerializeField] private float _jumpLift = 2;
    [SerializeField] private float _jumpDuration = 0.8f;
    [SerializeField] private float _jumpOffset = 4;
    [SerializeField] private GameObject _dustParticle;
    private bool isTriggered = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collected")
        {
            ThrowCups(other.gameObject);
            DustParticleSpawner();
            isTriggered = true;
        }
    }


    private void ThrowCups(GameObject touchedCup)
    {
        var cupsParent = touchedCup.transform.parent;       //cupsParent = environment
        //var cupIndex = touchedCup.transform.GetSiblingIndex();
        var cupIndex = NodeManager.Instance.nodes.IndexOf(touchedCup);

        while (cupIndex < NodeManager.Instance.nodes.Count)
        {
            var thrownCup = NodeManager.Instance.nodes[cupIndex];
            Destroy(thrownCup.GetComponent<NodeController>());
            Destroy(thrownCup.GetComponent<CollectController>());
            thrownCup.GetComponent<BoxCollider>().isTrigger = true;
            var rb = thrownCup.GetComponent<Rigidbody>();

            var dropPos = new Vector3(thrownCup.transform.position.x + Random.Range(-1.5f, 1.5f),
                                        thrownCup.transform.position.y,
                                        thrownCup.transform.position.z + _jumpOffset + Random.Range(0, 5f));

            rb.transform.DOJump(dropPos, _jumpLift, 1, _jumpDuration);

            thrownCup.tag = "Collectable";
            NodeManager.Instance.nodes.RemoveAt(cupIndex);


            if (NodeManager.Instance.nodes.Count == 0)
            {
                NodeManager.Instance.lastNode = NodeManager.Instance.Player.transform;
            }
            else
            {
                NodeManager.Instance.lastNode = NodeManager.Instance.nodes[NodeManager.Instance.nodes.Count - 1].transform;
            }
        }
    }

    private void DustParticleSpawner()
    {
        Instantiate(_dustParticle, this.transform.position, Quaternion.identity);
    }


}
