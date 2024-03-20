using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{

    public int baseMoveSpeed;
    public int dashCoef;

    private int moveSpeed;
    private bool dashing = false;
    private bool canDash = true;
    private float timerDash = 0;
    private bool timerDashOn = false;

    void Start()
    {
        moveSpeed = baseMoveSpeed;
    }

    void Update()
    {

        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            dashing = true;
        }

        if (dashing)
        {
            timerDashOn = true;
            dashing = false;
            for( float i = 0; i <= 0.2f; i += Time.deltaTime)
            {
                Vector2 dash = new(horizontalMovement * dashCoef, verticalMovement * dashCoef);
                transform.Translate(dash);
            }
        }


        if (timerDashOn)
        {
            timerDash += Time.deltaTime;
            canDash = false;

            if (timerDash > 0.2f)
            {
                timerDashOn = false;
                canDash = true;
                timerDash = 0f;
            }
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }

        if (!dashing)
        {
            MovePlayer(horizontalMovement, verticalMovement);
        }
    }

    void MovePlayer(float horizontalMovement, float verticalMovement)
    {
        Vector2 dir = new(horizontalMovement, verticalMovement);
        transform.Translate(dir);

        Debug.Log(dir);

    }
}
