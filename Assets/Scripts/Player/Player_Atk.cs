using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player_Atk : MonoBehaviour
{
    private GameObject player;

    public bool isColliding = false ;
    public List<GameObject> collidingWith;
    public float hitForce;
    public int damage = 25;
    private bool canHit;
    public bool isHiting;

    private float hitingCounter;
    public float hitingLenght;

    public Vector2 direction;
    public Vector2 target;

    public GameObject sword;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    void Update()
    {

        //////////////////////////// - ATK - ////////////////////////////

        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(sword, transform.position, Quaternion.identity, transform);
        }

        foreach (var item in collidingWith)
        {
            if (Input.GetKeyDown(KeyCode.C) && canHit)
            {
                isHiting = true;
                Enemy_Health enemy_Health = item.transform.GetComponent<Enemy_Health>();
                enemy_Health.TakeDamage(damage);
            }

            if (isHiting)
            {
                if (hitingCounter <= 0)
                {
                    hitingCounter = hitingLenght;
                }
                canHit = false;
                isHiting = false;
            }

            if (hitingCounter > 0)
            {
                hitingCounter -= Time.fixedDeltaTime;

                if (hitingCounter < 0)
                {
                    canHit = true;
                }
            }
        }

    }

    //////////////////////////// - Coll Detect - ////////////////////////////


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collidingWith.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collidingWith.Remove(collision.gameObject);
        }
    }

    /////////////////////////////////////////////////////////////////////
    ///
    ///   Player ATK Knockback : Add Knockback force + divide until 0
    ///   
    ///   Swipe ATK : hitbox instentiate + rotate
    /// 
    /////////////////////////////////////////////////////////////////////
}
