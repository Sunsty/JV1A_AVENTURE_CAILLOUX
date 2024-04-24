using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawner : MonoBehaviour
{

    void Awake()
    {
        Debug.Log("##########");
        GameObject.FindWithTag("Player").transform.position = this.transform.position;
    }
}
