using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawner : MonoBehaviour
{

    void Awake()
    {
        GameObject.FindWithTag("Player").transform.position = this.transform.position;
    }
}
