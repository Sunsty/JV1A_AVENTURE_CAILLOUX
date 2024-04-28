using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Environment_Encounter : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;

    public GameObject[] enemies;
    public GameObject[] doors;

    public int wavesAmount;

    public GameObject[] activeEnemies;

    public List<GameObject> listSP;

    private bool isFinished;
    private float finishCounter;
    public float finishLenght;
    private bool activated;

    private bool inCooldown;
    private float cooldownCounter;
    public float cooldownLenght;
    private bool cooldown;

    private void Awake()
    {
        foreach (var item in doors)
        {
            item.GetComponent<Animator>().SetBool("Opening", true);
            item.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if (activated)
        {
            activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            ////////////////////////////////////

            if (wavesAmount > 0)
            {
                if (activeEnemies.Length == 0)
                {
                    inCooldown = true;
                }

                if (inCooldown)
                {
                    if (cooldownCounter <= 0)
                    {
                        cooldownCounter = cooldownLenght;
                    }
                    cooldown = true;

                    inCooldown = false;
                }

                if (cooldownCounter > 0)
                {
                    cooldownCounter -= Time.fixedDeltaTime;
                }

                if (cooldownCounter < 0)
                {
                    cooldown = false;
                }

                ////////////////////////////////////

                if (activeEnemies.Length == 0 && !cooldown)
                {
                    SpawnEnemies();
                    wavesAmount -= 1;
                    isFinished = true;
                }
            }

            ////////////////////////////////////

            if (isFinished)
            {
                if (finishCounter <= 0)
                {
                    finishCounter = finishLenght;
                }

                isFinished = false;
            }

            if (finishCounter > 0)
            {
                finishCounter -= Time.fixedDeltaTime;

            }

            if (finishCounter < 0)
            {
                if (wavesAmount == 0 && activeEnemies.Length == 0)
                {
                    foreach (var item in doors)
                    {
                        item.GetComponent<Animator>().SetBool("Opening", true);
                        item.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
        }
        
    }



    private void SpawnEnemies()
    {
        int i = 0;
        foreach (var item in listSP)
        {
            Instantiate(enemies[i%(enemies.Length)], item.transform.position, Quaternion.identity);
            i++;
        }
    }

    public void Activate()
    {
        activated = true;
        foreach (var item in doors)
        {
            item.GetComponent<Animator>().SetBool("Opening", false);
            item.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
