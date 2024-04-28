using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Coin_Behavior : MonoBehaviour
{
    private bool follow;
    public GameObject player;
    public float moveSpeed;
    public Player_Inventory inventory;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponentInParent<Player_Inventory>();
    }

    private void FixedUpdate()
    {
        if (follow)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 0.6f)
        {
            inventory.AddCoin(1);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin_Collect"))
        {
            follow = true;
        }
    }
}
