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
    public Player_Health health;

    public float enemyDMG = 15;

    private bool enemyCollision;
    private bool canTakeDmg = true;

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

        if (enemyCollision && canTakeDmg)
        {
            timer.LoseTime(enemyDMG);
            health.TakeDmg();
        }

        //////////////////////////// - ////////////////////////////

        enemyCollision = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Encounter"))
        {
            collision.gameObject.GetComponentInParent<Environment_Encounter>().Activate();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyCollision = true;
        }
    }

    public void SetCanTakeDmg(bool state)
    {
        canTakeDmg = state;
    }
}
