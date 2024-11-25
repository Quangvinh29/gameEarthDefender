using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenuControl : MonoBehaviour
{
    public Image OptionMenu;

    public Scrollbar MusicVolume; 
    private const string MusicvolumeKey = "BackgroundMusicVolume";
    private VolumeChange VolumeC;

    public Scrollbar SoundEffectVolume;
    private const string Effectvolumekey = "SoundEffectMusicVolume";
    private SoundEffectVolumeChange[] VolumeShoot;

    
    public void Start()
    { 
        VolumeC = GameObject.Find("BackGround").GetComponent<VolumeChange>();

        SettingVolume();
        SettingEffectVolume();
    }

    // Su dung PlayerPrefs de dong bo cai dat giua cac menu cai dat
    public void SettingVolume()
    {
        if (PlayerPrefs.HasKey(MusicvolumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(MusicvolumeKey);
            MusicVolume.value = savedVolume;
        }
        else
        {
            MusicVolume.value = 1f;
        }

        // thuc hien kiem tra thay doi neu nhu scroll-bar thay doi
        MusicVolume.onValueChanged.AddListener(SetVolume);
    }

    // thiet lap cai dat gia tri moi cho scroll-bar o muc do tuong ung cho am nhac
    public void SetVolume(float value)
    {
        PlayerPrefs.SetFloat(MusicvolumeKey, value);
        PlayerPrefs.Save(); 

        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            VolumeC.DieuChinhVolumeMusic();
        }
    }

    //Su dung PlayerPrefs de dong bo cai dat giua cac menu cai dat
    public void SettingEffectVolume()
    {
        if (PlayerPrefs.HasKey(Effectvolumekey))
        {
            float savedEffectVolume = PlayerPrefs.GetFloat(Effectvolumekey);
            SoundEffectVolume.value = savedEffectVolume;
        }
        else
        {
            SoundEffectVolume.value = 1f;
        }

        SoundEffectVolume.onValueChanged.AddListener(SetEffectVolume);
    }

    // thiet lap cai dat gia tri moi cho scroll-bar o muc do tuong ung cho hieu ung am thanh
    public void SetEffectVolume(float value)
    {
        PlayerPrefs.SetFloat(Effectvolumekey, value);
        PlayerPrefs.Save();

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            VolumeShoot = GameObject.FindGameObjectsWithTag("PlayerShootDirection").Select(go => go.GetComponent<SoundEffectVolumeChange>()).ToArray();

            foreach (var volume in VolumeShoot)
            {
                volume.DieuChinhVolumeSoundEffect();
            }
        }
    }


    public void ExitOptionMenu()
    {
        OptionMenu.gameObject.SetActive(false);
    }
}

