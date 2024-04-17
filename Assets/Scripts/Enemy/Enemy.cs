using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject gmObject;
    public Game_Manager game_manager;
    public Enemy_IA enemy_IA;
    void Update()
    {
        gmObject = GameObject.FindGameObjectWithTag("Game_Manager");
        game_manager = gmObject.GetComponent<Game_Manager>();
        enemy_IA = GetComponent<Enemy_IA>();

        if (game_manager.GetTimeStopState())
        {
            enemy_IA.enabled = true;
        }
        else
        {
            enemy_IA.enabled = false;
        }
    }
}
