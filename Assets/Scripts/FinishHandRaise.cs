using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FinishHandRaise : MonoBehaviour
{
    private float levelScore;
    [SerializeField] private HighScoreDisplayer highScoreDisplayer;
    private GameObject finishTowerUIParent;





    public void RaiseHandbyScore()
    {
        levelScore = GameManager.Instance.Money - GameManager.Instance.LevelStartScore;
        var height = 0.5f + (levelScore / 5) * 0.75f;    //0.5f güvenlik önlemi. score/5: 5'er 5'er ilerlemesinden geliyor. 0.75f her bir birim yüksekliği
        transform.DOMoveY(height, levelScore / 30);
        StartCoroutine(ShowHighScore(levelScore / 30));
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

        var playerAnimCont = GameObject.FindGameObjectWithTag("PlayerHandAnimController");
        playerAnimCont.GetComponent<Animator>().SetBool("isThumbsUp", true);

        yield return new WaitForSeconds(0.7f);      //okay işaretinin tamamlanmasını bekle.

        //KAMWERAYI HIGHSCORE SEVİYESİNE YÜKSELT.
        highScoreDisplayer.HighScoreItemFinder();

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
