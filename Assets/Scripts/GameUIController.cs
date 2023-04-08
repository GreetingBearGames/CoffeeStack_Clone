using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text moneyText;


    private int _money;
    public int money
    {
        get { return _money; }
        set { 
            money = value; 
            moneyText.text = money.ToString();  //Para değeri değiştiğinde UI daki text değişecek.
            }
    }
    


    private void Start() {
        levelText.text = "Lv. " + (SceneManager.GetActiveScene().buildIndex+1).ToString();
    }

    public void OpenSettings(){
        settingsCanvas.SetActive(true);
    }

}
