using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float remainingTime;
    public Color color;

    void Start()
    {

    }

    void Update()
    {
        if (remainingTime > 0.1f)
        {
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 0.1f)
        {
            remainingTime = 0;
        }

        if (remainingTime < 15)
        {
            timerText.color = color;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
