using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawner : MonoBehaviour
{

    void Awake()
    {
        GameObject.FindWithTag("Player_Parent").GetComponentInParent<Transform>().position = this.transform.position;
        GameObject.FindWithTag("MainCamera").transform.position = this.transform.position;
    }
}
