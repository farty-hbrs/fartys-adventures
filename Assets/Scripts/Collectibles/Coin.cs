using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public string coinName;
    private LevelManager levelManager;

    void Start()
    {
        // When already collected -> destroy gameObject
        name = "coin-" + coinName;
        if(PlayerPrefs.GetInt(name) == 1)
        {
            Destroy(gameObject);
        }
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt(name, 1);
            levelManager.CollectCoin();
            Destroy(gameObject);
        }
    }
}
