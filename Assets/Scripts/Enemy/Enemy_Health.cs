using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int health = 100;
    private bool gotHit;
    public GameObject coin;

    void Update()
    {
        if (gotHit)
        {
            Enemy_IA enemy_IA = GetComponent<Enemy_IA>();
            enemy_IA.GetHit();
            gotHit = false;
        }

        if (health <= 0)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        gotHit = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }
}
