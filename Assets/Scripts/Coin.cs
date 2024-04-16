using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool asSpawned;
    public float spawningCounter;
    public float spawningLenght;

    private void Awake()
    {
        asSpawned = true;
    }

    private void Update()
    {
        if (asSpawned)
        {
            if (spawningCounter <= 0)
            {
                spawningCounter = spawningLenght;
            }
            asSpawned = false;
        }

        if (spawningCounter > 0)
        {
            spawningCounter -= Time.fixedDeltaTime;

            if (spawningCounter < 0)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }
}
