using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Android;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float remainingTime;
    public float timeLimit;
    public Color colorBase;
    public Color colorHurry;

    public bool isFinished;
    public float finishedCounter;
    public float finishedLenght;

    public GameObject gmObject;
    public Game_Manager game_manager;

    public bool paused = false;

    private void Start()
    {
        gmObject = GameObject.FindGameObjectWithTag("Game_Manager");
        game_manager = gmObject.GetComponent<Game_Manager>();

        remainingTime = timeLimit;
    }

    void Update()
    {

        if (game_manager.GetTimeStopState())
        {
            paused = false;
        }
        else
        {
            paused = true;
        }

        if (!paused)
        {
            if (remainingTime > 0.1f)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0.1f)
            {
                remainingTime = 0;
                isFinished = true;
            }

            if (remainingTime < 15)
            {
                timerText.color = colorHurry;
            }
            else
            {
                timerText.color = colorBase;
            }

            ////////////////////////

            if (isFinished)
            {
                if (finishedCounter <= 0)
                {
                    finishedCounter = finishedLenght;
                }
                isFinished = false;
            }

            if (finishedCounter > 0)
            {
                finishedCounter -= Time.fixedDeltaTime;

                if (finishedCounter < 0)
                {
                    remainingTime = timeLimit;
                }
            }

            ////////////////////////
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }

    public float GetTime()
    {
        return remainingTime;
    }
}
