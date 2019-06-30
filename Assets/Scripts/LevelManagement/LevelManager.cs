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
    public Text deathsText;
    public Text timeText;
    public float time;
    public GameObject[] objectsToResetWhenDead;

    private int deaths;
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
        deaths = PlayerPrefs.GetInt("Deaths", 0);
        deaths = (deaths > 0 && deaths < 999) ? deaths : 0;
        deathsText.text = deaths.ToString();

        coins = PlayerPrefs.GetInt("Coins", 0);
        coins = coins > 0 ? coins : 0;
        coinsText.text = coins.ToString();
    }
    
    void Update()
    {
        time -= Time.deltaTime;
        if (timeText)
        {
            timeText.text = ((int)time).ToString();
        }
        if (time <= 0f)
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        deaths++;
        PlayerPrefs.SetInt("Deaths", deaths);
        deathsText.text = "" + deaths;
        
        player.transform.position = currentPos;
        foreach(GameObject obj in objectsToResetWhenDead)
        {
            if(obj != null)
            {
                ResettableGameobject script = obj.GetComponent<ResettableGameobject>();
                if (script != null)
                {
                    script.Reset();
                }
            }
        }
    }

    public void CollectCoin()
    {
        coins++;
        PlayerPrefs.SetInt("Coins", coins);
        coinsText.text = "" + coins;
    }
}
