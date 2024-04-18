using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject gmObject;
    public Game_Manager game_manager;
    public Timer timer;

    public float enemyDMG = 15;

    private bool enemyCollision;
    private bool dmgCooldown = true;

    private bool isHit;
    private float hitCounter;
    public float hitLenght;

    void Awake()
    {
        gmObject = GameObject.FindGameObjectWithTag("Game_Manager");
        game_manager = gmObject.GetComponent<Game_Manager>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            timer.LoseTime(15);
        }

        //////////////////////////// - ////////////////////////////


        if (Input.GetKeyDown(KeyCode.G))
        {
            game_manager.SwitchTime();
        }

        //////////////////////////// - ////////////////////////////

        if ( enemyCollision && dmgCooldown )
        {
            timer.LoseTime(enemyDMG);
            isHit = true;
        }

        if (isHit)
        {
            if (hitCounter <= 0)
            {
                hitCounter = hitLenght;
            }

            dmgCooldown = false;

            isHit = false;
        }

        if (hitCounter > 0)
        {
            hitCounter -= Time.fixedDeltaTime;

            if (hitCounter < 0)
            {
                dmgCooldown = true;
            }
        }

        //////////////////////////// - ////////////////////////////

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyCollision = true;
        }

        if (collision.CompareTag("Encounter"))
        {
            collision.gameObject.GetComponent<Environment_Encounter>().Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyCollision = false;
        }
    }
}
