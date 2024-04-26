using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.GraphicsBuffer;

public class Enemy_IA : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject player;
    public float moveSpeed;

    public Vector3 knockback;
    public float knockbackRatio;

    public bool canMove = true;

    private bool charging;
    private bool idle;
    private float timerCharge;

    private float distance;

    private float activeMoveSpeed;
    public float dashSpeed;
    public float chargeCooldown;

    public float dashLength = .5f;
    private float dashCounter;

    public float idleLenght = .5f;
    private float idleCounter;

    private Vector2 target;

    public float orbitRange, orbitWidth;

    public bool gotHit;
    public float gotHitCounter;
    public float gotHitLength;
    private bool canGetHit = true;

    private int turnDir = 1;
    public bool inversed;

    void Awake()
    {
        activeMoveSpeed = moveSpeed;
        player = GameObject.FindGameObjectWithTag("Player");

        if (inversed)
        {
            turnDir = -1;
        }
        else
        {
            turnDir = 1;
        }
    }

    void Update()
    {
        
        //////////////////////////// - Get Hit - ////////////////////////////

        if (gotHit)
        {
            if (gotHitCounter <= 0)
            {
                gotHitCounter = gotHitLength;
            }

            knockback = -(knockbackRatio)*(transform.position - player.transform.position);

            canGetHit = false;
            canMove = false;
            gotHit = false;
        }

        if (gotHitCounter > 0)
        {
            gotHitCounter -= Time.fixedDeltaTime;

            rb.AddForce(knockback);
            knockback /= 1.1f;

            if (gotHitCounter < 0)
            {
                knockback = Vector3.zero;
                canMove = true;
                canGetHit = true;
            }
        }

        //////////////////////////// - Base Move - ////////////////////////////

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if (canMove)
        {
            if (distance > orbitRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, activeMoveSpeed * Time.fixedDeltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
            else if (distance > orbitRange - orbitWidth && distance < orbitRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + (turnDir) * Vector2.Perpendicular(direction), activeMoveSpeed * Time.fixedDeltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, -player.transform.position, activeMoveSpeed * Time.fixedDeltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
        }

        //////////////////////////// - Charge ATK - ////////////////////////////

        timerCharge += Time.deltaTime;

        if (timerCharge > chargeCooldown)
        {
            timerCharge = 0f;
            idle = true;
            idleCounter = 0;
        }

        //////////// - Idle - ////////////

        if (idle)
        {
            if (idleCounter <= 0)
            {
                idleCounter = idleLenght;
                target = player.transform.position + (player.transform.position - transform.position);
            }

            idle = false;
        }

        if (idleCounter > 0)
        {
            canMove = false;
            idleCounter -= Time.fixedDeltaTime;

            if (idleCounter < 0)
            {
                charging = true;
            }
        }

        //////////// - Charge - ////////////

        if (charging)
        {
            if (dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }

            charging = false;
        }

        if (dashCounter > 0)
        {
            canMove = false;
            dashCounter -= Time.fixedDeltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target, activeMoveSpeed * Time.fixedDeltaTime);

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
            }
        }

        if (dashCounter < 0 && idleCounter < 0 && gotHitCounter <= 0)
        {
            canMove = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword") && canGetHit)
        {
            gameObject.GetComponent<Enemy_Health>().TakeDamage(50);
            gotHit = true;
        }
        
        if (collision.CompareTag("Projectile") && canGetHit)
        {
            gameObject.GetComponent<Enemy_Health>().TakeDamage(25);
            gotHit = true;
        }
    }
}
