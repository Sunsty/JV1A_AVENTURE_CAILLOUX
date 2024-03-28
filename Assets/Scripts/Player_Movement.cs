using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{

    public Rigidbody2D rb;


    public float horizontalMovement;
    public float verticalMovement;    

    public float moveSpeed;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCooldownCounter;

    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {

        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        Vector2 target = new(transform.position.x + horizontalMovement, transform.position.y + verticalMovement);

        Debug.Log(target);

        Vector2 lookAngle = target - (Vector2)transform.position;

        if (lookAngle != Vector2.zero)
        {
            transform.right = lookAngle;
        }

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

        MovePlayer();

    }

    void MovePlayer()
    {
        Vector3 dir = new Vector3(horizontalMovement, verticalMovement, 0).normalized * activeMoveSpeed * Time.fixedDeltaTime;
        transform.position += dir;
    }
}
