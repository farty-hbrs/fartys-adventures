using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneOnClick : MonoBehaviour
{
    public string sceneToLoad;

    public void StartLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
