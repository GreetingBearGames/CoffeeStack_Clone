using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEndUIController : MonoBehaviour
{
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject levelText;

    [SerializeField] TMP_Text moneyText;

    private int _moneyAmount;
    public int moneyAmount
    {
        get { return _moneyAmount; }
        set { _moneyAmount = value; 
            moneyText.text = moneyAmount.ToString();
            //!Toplanan para alınıp texte atılacak
        }
    }
    
    
    private void OnEnable() {
        settingsButton.SetActive(false);
        levelText.SetActive(false);
    }

    public void GetMoney(){
        settingsButton.SetActive(true);
        levelText.SetActive(true);
        //! GameManager next level kodu gelecek         
    }
}
