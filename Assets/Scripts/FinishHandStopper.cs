using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishHandStopper : MonoBehaviour
{
    [SerializeField] private FinishHandRaise _finishHandRaise;
    private AutoMove_Demo _autoMove_Demo;
    private static bool isTriggeredonlyOnce = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" && !isTriggeredonlyOnce)
        {
            isTriggeredonlyOnce = true;
            _autoMove_Demo = GameObject.FindGameObjectWithTag("Player").GetComponent<AutoMove_Demo>();
            _autoMove_Demo.SpeedChanger(0, 0.2f);

            StartCoroutine("CameraMovement");
        }
    }


    IEnumerator CameraMovement()
    {
        yield return new WaitForSeconds(1);
        //KAMERAYI HAREKET ETTİR
        //KAMERA HAREKETİ BİTİNCE BİR MİKTAR SÜRE BEKLE
        _finishHandRaise.RaiseHandbyScore();
        this.transform.parent.SetParent(_autoMove_Demo.gameObject.transform);   //böylece el ile beraber yükselebilecek
    }
}
