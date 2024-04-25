using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sword_Swing : MonoBehaviour
{
    public GameObject player;

    private bool alive;
    private float aliveCounter;
    public float aliveLenght;

    private bool terminate;

    private bool swing;
    private float timeCount;

    private bool swingEnd;
    private float swingEndCounter;
    public float swingEndLenght;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        swing = true;
        alive = true;
    }

    void Update()
    {
        if (alive)
        {
            if (aliveCounter <= 0)
            {
                aliveCounter = aliveLenght;
            }

            alive = false;
        }

        if (aliveCounter > 0)
        {
            aliveCounter += Time.fixedDeltaTime;

            if (aliveCounter < 0)
            {
                terminate = true;
            }
        }

        if (swing)
        {
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(player.transform.localRotation.x * 360f, player.transform.localRotation.y * 360f, player.transform.localRotation.z * 360f) , Quaternion.Euler(player.transform.localRotation.x * 360f, player.transform.localRotation.y * 360f, player.transform.localRotation.z * 360f - 150f) , timeCount);
            timeCount += Time.fixedDeltaTime;

            if (terminate)
            {
                Debug.Log("End");
                swingEnd = true;

                if (swingEnd)
                {
                    if (swingEndCounter <= 0)
                    {
                        swingEndCounter = swingEndLenght;
                    }

                    swingEnd = false;
                }

                if (swingEndCounter > 0)
                {
                    swingEndCounter -= Time.fixedDeltaTime;

                    if (swingEndCounter < 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
