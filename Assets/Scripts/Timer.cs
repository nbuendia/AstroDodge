using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    int timerStart;
    public int timer;
    public int min;
    public int sec;
    public int deathMin;
    public int deathSec;

    public bool isRunning;

    UIManager uiManager;

    void Start()
    {
        //start round timer
        timerStart = (int)Time.time; //+ 60;
        isRunning = true;

        uiManager = GameObject.Find("BackgroundCanvas").GetComponent<UIManager>();
    }

    void Update()
    {
        //starts game timer and adjusts timer text
        if (isRunning)
        {
            timer = timerStart + (int)Time.time;
            min = timer / 60;
            sec = timer % 60;

            uiManager.timerText.text = min + ":" + sec.ToString("0#");
        }
    }
}
