using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueController : MonoBehaviour
{
    [SerializeField] TMP_Text valueText;

    private int _value;
    public int value
    {
        get { return _value; }
        set { _value = value; 
              valueText.text = value.ToString() +" $";
            }
    }

    private void Start() {
        value = 0;
    }

}
