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

            StartCoroutine("CameraMovement");
        }
    }


    IEnumerator CameraMovement()
    {
        yield return new WaitForSeconds(1);
        //KAMERAYI HAREKET ETTİR
        //KAMERA HAREKETİ BİTİNCE BİR MİKTAR SÜRE BEKLE
        _finishHandRaise.RaiseHandbyScore();
        this.transform.parent.SetParent(GameObject.FindGameObjectWithTag("Player").gameObject.transform);   //böylece el ile beraber yükselebilecek

    }

}
