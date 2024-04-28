using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Projectile_Trigger : MonoBehaviour
{
    private bool activated;

    public GameObject[] doors;

    void Update()
    {
        if (activated)
        {
            foreach (var item in doors)
            {
                item.GetComponent<Animator>().SetBool("Opening", true);
                item.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            foreach (var item in doors)
            {
                item.GetComponent<Animator>().SetBool("Opening", false);
                item.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void Activate()
    {
        activated = true;
    }
}
