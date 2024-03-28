using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.GraphicsBuffer;

public class Enemy_IA : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    private bool canMove = true;

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

    void Start()
    {
        canMove = true;
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {

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
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + Vector2.Perpendicular(direction), activeMoveSpeed * Time.fixedDeltaTime);
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

        if (dashCounter < 0 && idleCounter < 0)
        {
            canMove = true;
        }

    }

}
