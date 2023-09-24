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

    AudioManager audioManager;
    [SerializeField] private AudioSource loseGame;
    [SerializeField] private AudioSource timer;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        time += Time.deltaTime;
        float remainingTime = totalTime - time;


        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        string minutesString = minutes.ToString("D2");
        string secondsString = seconds.ToString("D2");

        timerText.text = ("Tempo: " + minutesString + ":" + secondsString);

        if (remainingTime <= 0)
        {
            Time.timeScale = 0;
            timerText.text = "Tempo: 00:00";
            finalScreen.SetActive(true);
            audioManager.StopMusic();
            loseGame.gameObject.SetActive(true);
            timer.gameObject.SetActive(false);
        }

    }

}
