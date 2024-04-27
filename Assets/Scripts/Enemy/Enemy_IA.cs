using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.GraphicsBuffer;


/// <summary>
/// Au lieu de position --> calculer direction puis aposer temps
/// </summary>
public class Enemy_IA : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

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

    private Vector3 newPosition;

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

    private void FixedUpdate()
    {
        if (canGetHit)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, activeMoveSpeed * Time.fixedDeltaTime);
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

            knockback = (knockbackRatio)*(transform.position - player.transform.position);

            rb.AddForce(knockback, ForceMode2D.Impulse);
            canGetHit = false;
            canMove = false;
            gotHit = false;
        }

        if (gotHitCounter > 0)
        {
            gotHitCounter -= Time.fixedDeltaTime;

            if (gotHitCounter < 0)
            {
                canMove = true;
                canGetHit = true;
                rb.velocity = Vector2.zero;
            }
        }

        //////////////////////////// - Base Move - ////////////////////////////




        if (canMove)
        {
            animator.SetBool("isCharging", false);

            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distance > orbitRange)
            {
                newPosition = player.transform.position;
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
            else if (distance > orbitRange - orbitWidth && distance < orbitRange)
            {
                newPosition = (Vector2)transform.position + (turnDir) * Vector2.Perpendicular(direction);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
            else
            {
                newPosition = -player.transform.position;
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

            canMove = false;

            idle = false;
        }

        if (idleCounter > 0)
        {
            idleCounter -= Time.fixedDeltaTime;

            newPosition = transform.position;

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

            animator.SetBool("isCharging", true);

            charging = false;
        }

        if (dashCounter > 0)
        {
            
            dashCounter -= Time.fixedDeltaTime;

            newPosition = target;

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
