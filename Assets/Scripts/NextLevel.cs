using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject target;
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LoadLevel();
        }
    }

    void LoadLevel()
    {
        // Mark level as mastered and save state
        if(PlayerPrefs.GetInt(sceneToLoad) == 0)
        {
            SceneManager.LoadScene(sceneToLoad);
            PlayerPrefs.SetInt(sceneToLoad, 1);
        }
    }
}
