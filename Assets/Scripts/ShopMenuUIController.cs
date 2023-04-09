using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuUIController : MonoBehaviour
{

    [SerializeField] GameObject CupHolder;

    [SerializeField] GameObject CarkHolder;
    
    public void CupButton(){
        CupHolder.SetActive(true);
        CarkHolder.SetActive(false);
    }

    public void CarkButton(){
        CupHolder.SetActive(false);
        CarkHolder.SetActive(true);
    }    
}
