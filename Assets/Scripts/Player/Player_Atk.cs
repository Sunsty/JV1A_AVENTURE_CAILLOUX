using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player_Atk : MonoBehaviour
{
    public bool isColliding = false ;
    public List<GameObject> collidingWith;
    public float hitForce;
    public int damage = 25;
    public bool isHiting;

    private float hitingCounter;
    public float hitingLenght;

    public Vector2 direction;
    public Vector2 target;

    void Update()
    {

        //////////////////////////// - ATK - ////////////////////////////

        foreach (var item in collidingWith)
        {
            if (Input.GetKeyDown(KeyCode.C) && !isHiting)
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
                isHiting = false;

                target = item.transform.position + (item.transform.position - transform.position);
            }

            if (hitingCounter > 0)
            {
                hitingCounter -= Time.fixedDeltaTime;

                item.transform.position = Vector2.MoveTowards(item.transform.position, target, hitForce * Time.fixedDeltaTime); ///~> AV <~///
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
