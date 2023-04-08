using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishHandRaise : MonoBehaviour
{
    [SerializeField] private float score;
    [SerializeField] private HighScoreDisplayer highScoreDisplayer;
    private GameObject finishTowerUIParent;





    public void RaiseHandbyScore()
    {
        var height = 0.5f + (score / 5) * 0.75f;    //0.5f güvenlik önlemi. score/5: 5'er 5'er ilerlemesinden geliyor. 0.75f her bir birim yüksekliği
        transform.DOMoveY(height, score / 30);
        StartCoroutine(ShowHighScore(score / 30));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ScoreTowerItem")
        {
            StartCoroutine(ChangeFinishTowerUIScale(other.gameObject));
            finishTowerUIParent = other.transform.parent.gameObject;
        }
    }


    IEnumerator ChangeFinishTowerUIScale(GameObject towerItem)
    {
        towerItem.transform.DOScaleX(1.2f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        towerItem.transform.DOScaleX(1, 0.3f);
    }


    IEnumerator ShowHighScore(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration + 0.1f);      //elin durmasını bekle.
        //KAMWERAYI HIGHSCORE SEVİYESİNE YÜKSELT.
        highScoreDisplayer.HighScoreItemFinder();
    }
}
