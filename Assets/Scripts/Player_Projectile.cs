using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 target;
    public Player_Movement playerMovement;
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        target = player.transform.position;

        if (Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(projectile, target, Quaternion.identity);
        }
    }
}
