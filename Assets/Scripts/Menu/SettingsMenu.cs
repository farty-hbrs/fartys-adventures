using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private float musicVol;
    private float fartVol;

    private void Start()
    {
        musicVol = PlayerPrefs.GetFloat("musicVol", 0);
        fartVol = PlayerPrefs.GetFloat("fartVol", 0);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVol", volume);
        PlayerPrefs.SetFloat("musicVol", volume);
    }

    public void SetFartVolume(float volume)
    {
        audioMixer.SetFloat("fartVol", volume);
        PlayerPrefs.SetFloat("fartVol", volume);
    }
}
