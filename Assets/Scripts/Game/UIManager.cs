using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.Localization;
using NUnit.Framework;
public class UIManager : MonoBehaviour
{
    public Image[] HealthUI;

    private PlayerMovement player;

    private Image Health;

    public LocalizedString LocalizedStringScore;
    public TMP_Text UIScore;
    private int score;

    public LocalizedString LocalizedStringEarthLifes;
    public TMP_Text EarthUI;
    private int life;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }

    // ham tang hinh 1 cuc mau khi bi tru mau
    public void HealthUpdate (int CurrentHealth)
    {
        Health = HealthUI[CurrentHealth - 1].GetComponent<Image>();
        Color color = Health.color;
        color.a = 0f;
        Health.color = color;
        CurrentHealth--;

    }

    // ham xoa tang hinh 1 cuc mau khi hoi mau
    public void PowerUpGetHealth(int CurrentHealth) 
    {
        if(CurrentHealth -1 >= 0)
        {
            Health = HealthUI[CurrentHealth - 1].GetComponent<Image>();
            Color color = Health.color;
            color.a = 1f;
            Health.color = color;
        }
    }

    //ham them 1 mau toi da, sau do tinh toan lai vi trí hien thi cua thanh mau
    public void AddHealth(int CurrentHealth, int MaxCurrentHealth)
    {
        RectTransform Health = GameObject.Find("Health 1").GetComponent<RectTransform>();
        RectTransform HealthBar = GameObject.Find("HealthBar").GetComponent<RectTransform>();

        int Healthdistance = 10;
        float Width = Health.rect.width;
        float totalWidth = (Width * MaxCurrentHealth) + Healthdistance;

        HealthBar.sizeDelta = new Vector2(totalWidth, HealthBar.sizeDelta.y);

        HealthBar.anchoredPosition = new Vector2(0, HealthBar.anchoredPosition.y);

        HealthCountAfterAddHealth(CurrentHealth, MaxCurrentHealth);
    }

    // ham tuy chinh mau se nhu the nao sau khi them 1 mau toi da neu dang day mau va dang it mau
    private void HealthCountAfterAddHealth(int CurrentHealth, int MaxCurrentHealth)
    {
        if( CurrentHealth  == MaxCurrentHealth)
        {
            HealthUI[MaxCurrentHealth-1].gameObject.SetActive(true);
            Health = HealthUI[MaxCurrentHealth - 1].GetComponent<Image>();
            Color color = Health.color;
            color.a = 1f;
            Health.color = color;
        }
        
        if ( CurrentHealth < MaxCurrentHealth) 
        {
            HealthUI[MaxCurrentHealth-1].gameObject.SetActive(true);
             Health = HealthUI[MaxCurrentHealth -1].GetComponent<Image>();
            Color color = Health.color;
            color.a = 0f;
            Health.color = color;
            player.HoiMau();
        }
    }

    // goi khi bat dau chay de tao luu tru du lieu diem theo localization sau moi lan choi lai  = 0
    // sau đo, dang ki su kien va thuc hien LocalizedStringScore.StringChanged lang nghe su thay doi trong chuoi de thuc hien UpdateText
    private void OnEnable()
    {
        LocalizedStringScore.Arguments = new object[] { score };
        LocalizedStringScore.StringChanged += ScoreUpdateText;

        LocalizedStringEarthLifes.Arguments = new object[] { life };
        LocalizedStringEarthLifes.StringChanged += EarthLifeUpdateText;
    }

    // neu nhan thay chuoi string co thay doi, thuc hien lay gia tri value chuoi moi va hien thi
    private void ScoreUpdateText(string value)
    {
        UIScore.text = value;
    }

    private void EarthLifeUpdateText(string value)
    {
        EarthUI.text = value;
    }

    // tang diem khi giet ke dich va RefreshString() lam moi chuoi de goi lai LocalizedStringScore.StringChanged += UpdateText;
    public void ScoreUpdate(int score)
    {
        LocalizedStringScore.Arguments[0] = score;
        LocalizedStringScore.RefreshString();

    }

    public void EarthLiveUpdate(int life)
    {
        LocalizedStringEarthLifes.Arguments[0] = life;
        LocalizedStringEarthLifes.RefreshString();
    }
}
