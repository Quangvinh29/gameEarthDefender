using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image[] HealthUI;

    [SerializeField]
    private TMP_Text ScoreUI, EarthUI;


    public void HealthUpdate(int CurrentLives)
    {
        HealthUI[CurrentLives].gameObject.SetActive(false);
    }

    public void ScoreUpdate(int TotalScore)
    {
        ScoreUI.text = "TotalScore: " + TotalScore.ToString();
    }

    public void EarthLiveUpdate(int EarthLives)
    {
        EarthUI.text = "Earth: " + EarthLives.ToString();
    }
}
