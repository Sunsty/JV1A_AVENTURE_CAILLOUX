using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sword_Swing : MonoBehaviour
{
    public GameObject player;

    private float angle;

    private bool swing;
    private float timeCount;

    private bool swingEnd;
    private float swingEndCounter;
    public float swingEndLenght;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        swing = true;
    }

    void Update()
    {

        if (swing)
        {
            angle = Mathf.Lerp(0, -150, timeCount);

            transform.localRotation = Quaternion.Euler(0, 0, angle);
            timeCount += Time.fixedDeltaTime;

            if (angle == -150)
            {
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
