using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{

    public int moveSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        MovePlayer(horizontalMovement, verticalMovement);
    }

    void MovePlayer(float horizontalMovement, float verticalMovement)
    {
        Vector2 dir = new(horizontalMovement, verticalMovement);
        transform.Translate(dir);
    }
}
