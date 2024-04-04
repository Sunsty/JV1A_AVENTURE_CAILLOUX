using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject gmObject;
    public Game_Manager game_manager;

    void Awake()
    {
        gmObject = GameObject.FindGameObjectWithTag("Game_Manager");
        game_manager = gmObject.GetComponent<Game_Manager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene( 1 , LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            game_manager.SwitchTime();
        }
    }

}
