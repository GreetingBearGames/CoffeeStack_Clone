using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] GameObject inputCanvas;
    [SerializeField] GameObject shopCanvas;

    [SerializeField] GameObject gameUICanvas;
    [SerializeField] GameObject leftButtonsGroup;
    [SerializeField] Transform leftGroupFinalPosition;

    void Update()
    {
        leftButtonsGroup.transform.DOMoveX(leftGroupFinalPosition.position.x,1f);
    }

    public void StartGame(){
        inputCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ShowShopMenu(){
        gameUICanvas.SetActive(false);
        shopCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ShowStartMenu(){
        gameUICanvas.SetActive(true);
        shopCanvas.SetActive(false);
        gameObject.SetActive(true);
    }


}
