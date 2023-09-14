using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuickTimeEvent : MonoBehaviour
{
    public int requiredClicks = 10; 
    private int currentClicks = 0;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            currentClicks++;

            if (currentClicks >= requiredClicks)
            {
                ReleasePlayer();
            }
        }
    }

    private void ReleasePlayer()
    {
        
        Debug.Log("Jogador se soltou da armadilha!");
        Destroy(gameObject); 
    }
}
