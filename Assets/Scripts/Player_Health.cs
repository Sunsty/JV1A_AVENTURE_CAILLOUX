using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public Timer timer;

    public Vector2 spawnPoint;
    public float health;

    void Start()
    {

    }

    void Update()
    {
        health = timer.GetTime();

        if (health == 0)
        {
            transform.position = spawnPoint;
        }

    }

    public void TakeDmg(float damage)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            spawnPoint = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
        }
    }
}
