using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider fartSlider;
    public Slider volSlider;

    private float musicVol;
    private float fartVol;

    private void Start()
    {
        musicVol = PlayerPrefs.GetFloat("musicVol", 0f);
        audioMixer.SetFloat("musicVol", musicVol);
        volSlider.SetValueWithoutNotify(musicVol);
        fartVol = PlayerPrefs.GetFloat("fartVol", 0f);
        audioMixer.SetFloat("fartVol", fartVol);
        fartSlider.SetValueWithoutNotify(musicVol);
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
