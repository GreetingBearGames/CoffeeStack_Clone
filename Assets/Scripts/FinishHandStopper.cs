using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishHandStopper : MonoBehaviour
{
    [SerializeField] private FinishHandRaise _finishHandRaise;
    [SerializeField] private MovementController movementController;
    private static bool isTriggeredonlyOnce = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isTriggeredonlyOnce)
        {
            isTriggeredonlyOnce = true;
            movementController.SpeedChanger(0, 0.2f);
            var playerAnimCont = GameObject.FindGameObjectWithTag("PlayerHandAnimController");
            playerAnimCont.GetComponent<Animator>().SetBool("isMoneyHold", true);

            StartCoroutine("CameraMovement");
        }
    }


    IEnumerator CameraMovement()
    {
        CameraManager.Instance.SwitchCamera(CameraManager.Instance.finish2Cam);
        yield return new WaitForSeconds(1);
        //KAMERAYI HAREKET ETTİR
        //KAMERA HAREKETİ BİTİNCE BİR MİKTAR SÜRE BEKLE
        _finishHandRaise.RaiseHandbyScore();
        this.transform.parent.SetParent(GameObject.FindGameObjectWithTag("Player").gameObject.transform);   //böylece paraların el ile beraber yükselebilecek

    }

}
