using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour
{
    private bool isTraveling;
    private float travelCounter;
    public float travelLenght;
    public float travelSpeed;

    private Player_Movement playerMovement;

    private GameObject player;
    private GameObject atkHitbox;

    private Vector3 dir;
    private Vector2 rota;


    void Awake()
    {
        playerMovement = FindAnyObjectByType<Player_Movement>();
        rota = playerMovement.GetLookAngle();

        player = GameObject.FindGameObjectWithTag("Player");
        atkHitbox = GameObject.FindGameObjectWithTag("ATK_Hitbox");

        dir =  atkHitbox.transform.position - player.transform.position;

        transform.right = rota;

        isTraveling = true;
    }

    void Update()
    {

        if (isTraveling)
        {
            if (travelCounter <= 0)
            {
                travelCounter = travelLenght;
            }
            isTraveling = false;
        }

        if (travelCounter > 0)
        {
            travelCounter -= Time.fixedDeltaTime;

            transform.position += dir/50 * travelSpeed;

            if (travelCounter < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
