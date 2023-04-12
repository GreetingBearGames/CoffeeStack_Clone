using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawUI : MonoBehaviour
{
    [SerializeField] GameObject startMenuUI;

    public void DrawNameSaveButton(){
        startMenuUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
