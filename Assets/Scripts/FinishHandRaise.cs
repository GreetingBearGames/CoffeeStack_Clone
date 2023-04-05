using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishHandRaise : MonoBehaviour
{
    [SerializeField] private float score;


    public void RaiseHandbyScore()
    {
        var height = 0.5f + (score / 5) * 0.75f;    //0.5f güvenlik önlemi. score/5: 5'er 5'er ilerlemesinden geliyor. 0.75f her bir birim yüksekliği
        transform.DOMoveY(height, score / 30);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ScoreTowerItem")
        {
            StartCoroutine(ChangeFinishTowerUIScale(other.gameObject));
        }
    }


    IEnumerator ChangeFinishTowerUIScale(GameObject towerItem)
    {
        towerItem.transform.DOScaleX(1.2f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        towerItem.transform.DOScaleX(1, 0.3f);
    }
}
