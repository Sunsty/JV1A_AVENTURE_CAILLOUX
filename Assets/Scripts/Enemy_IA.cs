using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_IA : MonoBehaviour
{
    public float speed;
    private Vector2 target;
    private Vector2 here;
    private GameObject objectToFollow;

    void Start()
    {
        objectToFollow = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        here = new(transform.position.x, transform.position.y);
        target = new(objectToFollow.transform.position.x, objectToFollow.transform.position.y);
        float step = speed * Time.deltaTime;

        if (Vector2.Distance(here, target) < 100f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, step);

        }

    }
}
