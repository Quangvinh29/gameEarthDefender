using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectVolumeChange : MonoBehaviour
{
    public AudioSource SoundEffect;
    private const string Effectvolumekey = "SoundEffectMusicVolume";

    private void Start()
    {
        DieuChinhVolumeSoundEffect();
    }
    public void DieuChinhVolumeSoundEffect()
    {
        if (PlayerPrefs.HasKey(Effectvolumekey))
        {
            float newVolumeEffect = PlayerPrefs.GetFloat(Effectvolumekey);
            SoundEffect.volume = newVolumeEffect;

        }
        else
        {
            SoundEffect.volume = 1f;
        }
    }
}
