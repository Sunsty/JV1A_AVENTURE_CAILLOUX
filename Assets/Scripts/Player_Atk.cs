using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player_Atk : MonoBehaviour
{
    public bool isColliding = false ;
    public GameObject collidingWith;
    public float hitForce;
    public int damage = 25;
    public bool isHiting;

    private float hitingCounter;
    public float hitingLenght;

    public Vector2 direction;
    public Vector2 target;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) && isColliding && !isHiting)
        {
            isHiting = true;
            Enemy_Health enemy_Health = collidingWith.transform.GetComponent<Enemy_Health>();
            enemy_Health.TakeDamage(damage);
        }

        if (isHiting)
        {
            if (hitingCounter <= 0)
            {
                hitingCounter = hitingLenght;
            }
            isHiting = false;

            target = collidingWith.transform.position + (collidingWith.transform.position - transform.position);
        }

        if (hitingCounter > 0)
        {
            hitingCounter -= Time.fixedDeltaTime;

            collidingWith.transform.position = Vector2.MoveTowards(collidingWith.transform.position, target, hitForce * Time.fixedDeltaTime);

            if (hitingCounter < 0)
            {

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isColliding = true;
            collidingWith = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isColliding = false;
        }
    }
}
