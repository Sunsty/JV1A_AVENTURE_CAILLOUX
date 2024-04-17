using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public int coinAmount;
    public List<GameObject> coinList;
    public TextMeshProUGUI coinDisplay;
    public float moveSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        coinDisplay.SetText(string.Format("{0} Coins", coinAmount));

        foreach (var item in coinList)
        {
            item.transform.position = Vector2.MoveTowards(item.transform.position, transform.position, moveSpeed * Time.fixedDeltaTime);
            if (item.transform.position == transform.position)
            {
                Destroy(item);
                coinList.Remove(item);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coinList.Add(collision.gameObject);
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            coinAmount += 1;
        }

        /*
        if (collision.CompareTag("PowerUp1"))
        {
            Destroy(collision.gameObject);

        }

        if (collision.CompareTag("PowerUp2"))
        {
            Destroy(collision.gameObject);

        }
        */
    }

}
