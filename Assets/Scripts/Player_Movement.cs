using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{

    public Rigidbody2D rb;

    public float baseMoveSpeed;
    public GameObject beacon;

    public float horizontalMovement;
    public float verticalMovement;    

    private float moveSpeed;
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

        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        Vector2 target = new(transform.position.x + horizontalMovement, transform.position.y + verticalMovement);
        beacon.transform.position = target;

        Debug.Log(target);

        Vector2 lookAngle = beacon.transform.position - transform.position;
        transform.right = lookAngle;

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            dashing = true;
        }

        if (dashing)
        {
            timerDashOn = true;
            dashing = false;
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

        if (!dashing)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        Vector3 dir = new Vector3(horizontalMovement, verticalMovement, 0).normalized * moveSpeed * Time.fixedDeltaTime;
        transform.position += dir;

    }
}
