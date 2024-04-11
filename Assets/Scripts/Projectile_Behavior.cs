using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
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

    public GameObject gmObject;
    private Game_Manager game_manager;

    private Vector3 dir;
    private Vector2 rota;

    bool paused;


    void Awake()
    {
        gmObject = GameObject.FindGameObjectWithTag("Game_Manager");
        game_manager = gmObject.GetComponent<Game_Manager>();

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
        if (game_manager.GetTimeStopState())
        {
            paused = false;
        }
        else
        {
            paused = true;
        }

        if (!paused)
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
/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision)
        collision.gameObject.GetComponent<Enemy_Health>().TakeDamage(10);
    }*/

}
