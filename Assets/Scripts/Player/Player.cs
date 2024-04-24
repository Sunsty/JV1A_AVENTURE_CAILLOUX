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

        if (enemyCollision && canTakeDmg)
        {
            timer.LoseTime(enemyDMG);
            health.TakeDmg();
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
            Debug.Log("######");
            collision.gameObject.GetComponentInParent<Environment_Encounter>().Activate();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyCollision = false;
        }
    }

    public void SetCanTakeDmg(bool state)
    {
        canTakeDmg = state;
    }
}
