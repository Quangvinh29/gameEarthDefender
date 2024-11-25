using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Localization.Settings;

public class LocaleSelection : MonoBehaviour
{
    private bool active = false;

    // thiet lap cai dat ngon ngu da chon theo lan choi truoc do
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

    // ham thuc hien 1 lan khi nhan nut thay doi ngon ngu voi gia tri truyen vao là ID cua ngon ngu do
    public void ChangeLocale(int ID)
    {
        if(active == false)
        {
            StartCoroutine(SetLocale(ID));
        }
    }

    // thuc hien thay doi ngon ngu theo ID
    IEnumerator SetLocale(int ID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[ID];
        PlayerPrefs.SetInt("LocaleKey", ID);
        active = false;
    }
}
