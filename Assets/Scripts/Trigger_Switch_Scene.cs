using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger_Switch_Scene : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scene 2", LoadSceneMode.Single);
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("Scene 2"));
        }
    }
}
