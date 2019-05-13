using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;
    public Vector3 currentPos;
    public GameObject player;
    public Text livesText;
    public Text timeText;
    public float time;

    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        lives = PlayerPrefs.GetInt("Lives");
        lives = lives > 0 ? lives : 5;
        livesText.text = "x" + lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = ((int)time).ToString();
        if(time <= 0f)
        {

        }
        if(CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    public void RespawnPlayer()
    {
        lives--;
        PlayerPrefs.SetInt("Lives", lives);
        livesText.text = "x" + lives;
        
        if(lives > 0)
        {
            player.transform.position = currentPos;
        }
        else
        {
            PlayerPrefs.SetInt("Coins", 0);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
