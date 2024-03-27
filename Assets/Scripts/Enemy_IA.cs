using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_IA : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;

    void Start()
    {

    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;



        if (distance > 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else if (distance > 2 && distance < 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.Perpendicular(direction), speed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
