using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Player_Health : MonoBehaviour
{
    public Timer timer;
    public SpriteRenderer sprite;
    public Player player;

    public Vector2 spawnPoint;
    public float health = 100;

    private bool takeDmg;

    private float takeDmgCounter;
    public float takeDmgLenght;

    void Update()
    {
        health = timer.GetTime();

        if (takeDmg)
        {
            if (takeDmgCounter <= 0)
            {
                takeDmgCounter = takeDmgLenght;
            }

            player.SetCanTakeDmg(false);

            takeDmg = false;
        }

        if (takeDmgCounter > 0)
        {
            takeDmgCounter -= Time.fixedDeltaTime;

            if (takeDmgCounter % 0.75f >= 0.325f)
            {
                sprite.color = new Color(255, 255, 255, 0);
            }
            else
            {
                sprite.color = new Color(255, 255, 255, 255);
            }

            if (takeDmgCounter <= 0)
            {
                player.SetCanTakeDmg(true);
            }
        }

        if (health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            timer.LoseTime(-1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            spawnPoint = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
        }
    }

    public void TakeDmg()
    {
        takeDmg = true;
    }

}
