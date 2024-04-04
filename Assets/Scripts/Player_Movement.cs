using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{

    public Rigidbody2D rb;


    private float horizontalMovement;
    private float verticalMovement;    

    public float moveSpeed;
    public float atkMSCoef;
    private float MSCoef = 1;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCooldownCounter;

    private bool isHiting;
    private float hitingCounter;
    public float hitingLenght;


    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {

        //////////////////////////// - Base Move - ////////////////////////////

        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        Vector2 target = new(transform.position.x + horizontalMovement, transform.position.y + verticalMovement);

        Vector2 lookAngle = target - (Vector2)transform.position;

        if (lookAngle != Vector2.zero)
        {
            transform.right = lookAngle;
        }

        //////////////////////////// - Dash - ////////////////////////////

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ( dashCooldownCounter <= 0 && dashCounter <= 0 )
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.fixedDeltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
            }
        }

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.fixedDeltaTime;
        }

        //////////////////////////// - ATK Slow - ////////////////////////////

        if ( Input.GetKeyDown(KeyCode.C))
        {
            isHiting = true;
        }

        if (isHiting)
        {
            if (hitingCounter <= 0)
            {
                hitingCounter = hitingLenght;
            }

            MSCoef = atkMSCoef;

            isHiting = false;
        }

        if (hitingCounter > 0)
        {
            hitingCounter -= Time.fixedDeltaTime;

            if (hitingCounter < 0)
            {
                MSCoef = 1;
            }
        }

        //////////////////////////// - Move Func - ////////////////////////////

        MovePlayer();

    }

    void MovePlayer()
    {
        Vector3 dir = new Vector3(horizontalMovement, verticalMovement, 0).normalized * ( activeMoveSpeed * MSCoef) * Time.fixedDeltaTime;
        transform.position += dir;
    }
}
