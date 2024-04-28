using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Locked_Door: MonoBehaviour
{
    private bool hasKey;
    public GameObject door;

    private void Update()
    {
        if (hasKey)
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.SetKeyState(false);

            door.GetComponent<Animator>().SetBool("Opening", true);
            door.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }

    public void SetHasKey(bool keyState)
    {
        hasKey = keyState;
    }
}
