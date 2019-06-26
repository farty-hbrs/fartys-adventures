using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;
    public Vector3 currentPos;
    public GameObject player;
    public Text coinsText;
    public Text livesText;
    public Text timeText;
    public float time;
    public GameObject[] objectsToResetWhenDead;

    private int lives;
    private int coins;
    private static LevelManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        lives = PlayerPrefs.GetInt("Lives", 5);
        livesText.text = lives.ToString();

        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
    }
    
    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = ((int)time).ToString();
        if(time <= 0f)
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        lives--;
        PlayerPrefs.SetInt("Lives", lives);
        livesText.text = "" + lives;
        
        if(lives > 0)
        {
            player.transform.position = currentPos;
            foreach(GameObject obj in objectsToResetWhenDead)
            {
                if(obj != null)
                {
                    ResettableGameobject script = obj.GetComponent<ResettableGameobject>();
                    if(script != null)
                    {
                        script.Reset();
                    }
                }
            }
        }
        else
        {
            //coins = 0;
            //PlayerPrefs.SetInt("Coins", coins);
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void CollectCoin()
    {
        coins++;
        PlayerPrefs.SetInt("Coins", coins);
        coinsText.text = "" + coins;
    }
}
