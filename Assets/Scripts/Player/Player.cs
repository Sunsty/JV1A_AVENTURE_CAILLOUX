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
    public GameObject key_display;

    public float enemyDMG = 15;

    private bool enemyCollision;
    private bool canTakeDmg = true;
    private bool hasPU1;
    private bool hasPU2;
    private bool hasKey;

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

        if (hasPU2) 
        { 
            if (Input.GetKeyDown(KeyCode.G))
            {
                game_manager.SwitchTime();
            }
        }
            

        //////////////////////////// - ////////////////////////////

        if (enemyCollision && canTakeDmg)
        {
            timer.LoseTime(enemyDMG);
            health.TakeDmg();
        }

        //////////////////////////// - ////////////////////////////

        enemyCollision = false;

        if (hasKey)
        {
            key_display.SetActive(true);
        }
        else
        {
            key_display.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Encounter"))
        {
            collision.gameObject.GetComponentInParent<Environment_Encounter>().Activate();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("PowerUp1")) 
        {
            hasPU1 = true;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("PowerUp2"))
        {
            hasPU2 = true;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Locked_Door"))
        {
            collision.gameObject.GetComponent<Locked_Door>().SetHasKey(hasKey);
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

    public bool GetPU1state()
    {
        return hasPU1;
    }

    public void SetKeyState(bool state)
    {
        hasKey = state;
    }
}
