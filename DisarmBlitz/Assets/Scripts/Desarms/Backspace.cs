using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Backspace : MonoBehaviour
{
    [SerializeField] private Button backspaceButton;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI result2Text;

    void Start()
    {
        backspaceButton.onClick.AddListener(EraseNumber);
    }

    void EraseNumber()
    {
        resultText.text = "";
        result2Text.text = "";
    }
}
