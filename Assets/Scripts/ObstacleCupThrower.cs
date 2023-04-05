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


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collected")
        {
            ThrowCups(other.gameObject);
            DustParticleSpawner();
        }
    }


    private void ThrowCups(GameObject touchedCup)
    {
        var cupsParent = touchedCup.transform.parent;
        var cupIndex = touchedCup.transform.GetSiblingIndex();

        while (cupIndex < cupsParent.childCount)
        {
            var thrownCup = cupsParent.GetChild(cupIndex);
            var rb = thrownCup.GetComponent<Rigidbody>();
            var dropPos = new Vector3(thrownCup.transform.position.x + Random.Range(-1.5f, 1.5f),
                                        thrownCup.transform.position.y,
                                        thrownCup.transform.position.z + _jumpOffset + Random.Range(0, 5f));

            rb.transform.DOJump(dropPos, _jumpLift, 1, _jumpDuration);

            thrownCup.transform.parent = this.transform;
            thrownCup.tag = "Collectable";
        }
    }

    private void DustParticleSpawner()
    {
        Instantiate(_dustParticle, this.transform.position, Quaternion.identity);
    }


}
