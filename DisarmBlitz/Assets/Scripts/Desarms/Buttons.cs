using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI thisText;

    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI result2Text;

    void Start()
    {
        button.onClick.AddListener(DisplayNumber);
    }

    void DisplayNumber()
    {
        if (resultText.text == "")
        {
            resultText.text = thisText.text;
        }

        else if(result2Text.text == "")
        {
            result2Text.text = thisText.text;
        }
        else
        {
            return;
        }
    }
}
