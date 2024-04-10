using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public int coinAmount;
    public TextMeshProUGUI coinDisplay;
    void Start()
    {
        
    }

    void Update()
    {
        coinDisplay.SetText(string.Format("{0} Coins", coinAmount));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coinAmount += 1;
        }

        if (collision.CompareTag("PowerUp1"))
        {
            Destroy(collision.gameObject);

        }

        if (collision.CompareTag("PowerUp2"))
        {
            Destroy(collision.gameObject);

        }
    }
}
