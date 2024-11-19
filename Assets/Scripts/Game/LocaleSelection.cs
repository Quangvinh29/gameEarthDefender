using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Localization.Settings;

public class LocaleSelection : MonoBehaviour
{
    private bool active = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("LocalID"))
        {
            return;
        }
        else
        {
            int ID = PlayerPrefs.GetInt("LocaleKey", 0);
            ChangeLocale(ID);
        }
    }
    public void ChangeLocale(int ID)
    {
        if(active == false)
        {
            StartCoroutine(SetLocale(ID));
        }
    }

    IEnumerator SetLocale(int ID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[ID];
        PlayerPrefs.SetInt("LocaleKey", ID);
        active = false;
    }
}
