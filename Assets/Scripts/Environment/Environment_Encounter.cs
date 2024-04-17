using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Environment_Encounter : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;

    public int wavesAmount;

    public List<GameObject> activeEnemies;

    public List<GameObject> listSP;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (wavesAmount > 0)
        {
            if (activeEnemies == null)
            {
                SpawnEnemies();
                wavesAmount -= 1;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
        activeEnemies.Add(collision.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
        activeEnemies.Remove(collision.gameObject);
        }
    }

    private void SpawnEnemies()
    {
        foreach (var item in listSP)
        {
            Instantiate(enemy1, item.transform.position, Quaternion.identity);
        }
    }
}
