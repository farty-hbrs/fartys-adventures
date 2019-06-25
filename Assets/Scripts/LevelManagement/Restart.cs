using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public bool restartScene;
    private LevelManager levelManager;
    
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (restartScene)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            levelManager.RespawnPlayer();
        }
    }
}
