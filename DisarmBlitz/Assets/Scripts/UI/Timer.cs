using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Timer : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI timerText;
    private float time;
    private float totalTime = 120;
    [SerializeField] private GameObject finalScreen;

    void Update()
    {
        time += Time.deltaTime;
        float remainingTime = totalTime - time;

        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        string minutesString = minutes.ToString("D2");
        string secondsString = seconds.ToString("D2");

        timerText.text = ("Time: " + minutesString + ":" + secondsString);
    
        if (remainingTime <= 0)
        {
            Time.timeScale = 0;
            timerText.text = "Time: 00:00";
            finalScreen.SetActive(true);

        }

    }

}   
