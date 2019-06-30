using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        /* Disable for open games day
        if (this.GetComponent<Button>() != null)
        {
            // Activate first level (new game)
            PlayerPrefs.SetInt("Hayascene", 1);

            // If script is attached to a button make it unclickable if level is not unlocked yet
            if (PlayerPrefs.GetInt(sceneToLoad) == 1)
            {
                this.GetComponent<Button>().interactable = true;
            }
            else
            {
                this.GetComponent<Button>().interactable = false;
            }
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt(sceneToLoad, 1);
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
