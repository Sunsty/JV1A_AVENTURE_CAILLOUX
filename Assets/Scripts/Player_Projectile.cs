using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 target;
    public Player_Movement playerMovement;

    void Start()
    {
        
    }

    void Update()
    {
        target = playerMovement.GetTarget();

        if (Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(projectile, target, Quaternion.identity);
        }
    }
}
