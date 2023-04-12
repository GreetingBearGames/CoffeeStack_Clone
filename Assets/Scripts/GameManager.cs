using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float _levelStartScore;
    private static GameManager _instance;   //Create instance and make it static to be sure that only one instance exist in scene.
    [SerializeField] private GameObject _gameFinishUI, _gameOverUI;
    [SerializeField] private TMP_Text gameUIMoneyText;


    public static GameManager Instance
    {     //To access GameManager, we use GameManager.Instance
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is null");
            }
            return _instance;
        }
    }
    public int Money
    {       //Money property. You can get money from outside this script, but you can only set in this script.
        get => PlayerPrefs.GetInt("Money", 0);
        private set => PlayerPrefs.SetInt("Money", value);
    }

    public int HighScore
    {       //HighScore property. You can get highscore from outside this script, but you can only set in this script.
        get => PlayerPrefs.GetInt("HighScore", 0);
        private set => PlayerPrefs.SetInt("HighScore", value);
    }

    private void Awake()
    {
        _instance = this;
        gameUIMoneyText.text = Money.ToString();
        LevelStartScore = Money;
    }

    public void UpdateMoney(int updateAmount)
    {     //To update money.Use positive value to increment. Use negative value to decrement.
        Money += updateAmount;
        if (Money < 0)
        {                           //To make sure that money is above 0.
            Money = 0;
        }

        if (Money - LevelStartScore > HighScore)
        {
            HighScore = Money;
        }

        gameUIMoneyText.text = Money.ToString();
    }

    public float LevelStartScore
    {
        get => _levelStartScore;
        set => _levelStartScore = value;
    }


    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0){
            SceneManager.LoadScene(1);
        }
        else{
            SceneManager.LoadScene(0);
        }
        
    }
    public void LoseLevel()
    {
        _gameOverUI.SetActive(true);
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishLevel()
    {
        _gameFinishUI.SetActive(true);
    }

    public void StartLevel()
    {

    }
}
