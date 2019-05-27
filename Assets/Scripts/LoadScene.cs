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
        // Activate first level (new game)
        PlayerPrefs.SetInt("scene00", 1);

        // If script is attached to a button make it unclickable if level is not unlocked yet
        if (PlayerPrefs.GetInt(sceneToLoad) == 1 && this.GetComponent<Button>() != null)
        {
            this.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.GetComponent<Button>().interactable = false;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
