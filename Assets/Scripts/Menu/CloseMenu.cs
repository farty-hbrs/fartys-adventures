using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CloseMenu : MonoBehaviour
{
    public GameObject mainMenu;

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
