using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Vector2 target;
    public float lookAngle;
    public Vector3 dir;

    public Animator animator;


    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (horizontalMovement == 0 && verticalMovement == 0)
        {
            animator.SetBool("Mooving", false);
        }
        else
        {
            animator.SetBool("Mooving", true);
        }

        //////////////////////////// - Base Move - ////////////////////////////

        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

/*        target = new Vector2(transform.position.x + horizontalMovement, transform.position.y + verticalMovement);
*/
        if (horizontalMovement + verticalMovement != 0)
        {
            target = new Vector2(horizontalMovement, verticalMovement);

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        
        
        

        //////////////////////////// - Dash - ////////////////////////////

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCooldownCounter <= 0 && dashCounter <= 0)
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

        if (Input.GetKeyDown(KeyCode.C))
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

        dir = new Vector3(horizontalMovement, verticalMovement, 0).normalized * (activeMoveSpeed * MSCoef) * Time.fixedDeltaTime;
        transform.position += dir;

    }

    public Vector2 GetLookAngle()
    {
        return transform.right;
    }

    public void SetMS(float ms)
    {

    }
}
