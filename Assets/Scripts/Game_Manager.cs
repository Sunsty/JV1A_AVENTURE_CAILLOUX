using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public GameObject[] element;

    public GameObject filter;

    public bool isTimeStopped;
    private bool switchTime;

    void Awake()
    {
        foreach(var item in element)
        {
            DontDestroyOnLoad(item);
        }
    }

    private void Update()
    {

        if (isTimeStopped && switchTime)
        {
            isTimeStopped = false;
            switchTime = false;
        }

        if (!isTimeStopped && switchTime)
        {
            isTimeStopped = true;
            switchTime = false;
        }

        if (isTimeStopped)
        {
            filter.SetActive(false);
        }
        else
        {
            filter.SetActive(true);
        }

       
    }
    public bool GetTimeStopState() 
    { 
        return isTimeStopped;
    }

    public void SwitchTime()
    {
        switchTime = true;
    }

}
