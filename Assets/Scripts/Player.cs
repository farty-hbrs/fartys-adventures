using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text coinText;
    //public Text livesText;
    //private int lives;
    private int coins;
    // Start is called before the first frame update
    void Start()
    {
        //lives = PlayerPrefs.GetInt("Lives");
        //lives = lives > 0 ? lives : 5;
        coins = PlayerPrefs.GetInt("Coins");
        coins = coins > 0 ? coins : 0;
        coinText.text = "x" + coins.ToString();
        //livesText.text = "Lives: " + lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            coins++;
            PlayerPrefs.SetInt("Coins", coins);
            coinText.text = "x" + coins.ToString();
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Checkpoint")
        {
            Destroy(collision.gameObject);
        }
        /*if(collision.tag == "Enemy" || collision.tag == "Killzone")
        {
            lives--;
            livesText.text = "Lives: " + lives.ToString();
        }*/
    }
}
