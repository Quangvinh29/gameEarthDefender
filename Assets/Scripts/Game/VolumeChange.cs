using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChange : MonoBehaviour
{
    public AudioSource BackGroundMusic;
    private const string MusicvolumeKey = "BackgroundMusicVolume";

    public void Start()
    {
        DieuChinhVolumeMusic();
    }

    public void DieuChinhVolumeMusic()
    {
        if (PlayerPrefs.HasKey(MusicvolumeKey))
        {
            float newVolume = PlayerPrefs.GetFloat(MusicvolumeKey);
            BackGroundMusic.volume = newVolume;
        }
        else
        {
            BackGroundMusic.volume = 1f;
        }
    }

}
