using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 target;
    public Player_Movement playerMovement;
    public Player player_script;
    public GameObject projectileSP;

    private bool canFire = true;
    private bool firing;
    private float firingCounter;
    public float firingLenght;

    void Update()
    {
        target = projectileSP.transform.position;

        if (player_script.GetPU1state())
        {
            if (Input.GetKeyDown(KeyCode.V) && canFire)
            {
                Instantiate(projectile, target, Quaternion.identity);
                firing = true;
            }
        }

        if (firing)
        {
            if (firingCounter <= 0f)
            {
                firingCounter = firingLenght;
            }
            
            canFire = false;

            firing = false;
        }

        if (firingCounter > 0f)
        {
            firingCounter -= Time.deltaTime;

            if (firingCounter < 0f)
            {
                canFire = true;
            }
        }
    }

}
