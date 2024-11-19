using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class Score : MonoBehaviour
{
    private int CurrentScore = 0;
    private UIManager ScoreUp;

    private void Start()
    {
        ScoreUp = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (ScoreUp == null)
        {
            Debug.Log("ScoreUp is Null!");
        }
    }

    public void AddScore(int Score)
    {
        CurrentScore += Score;
    }

    private void Update()
    {
        ScoreUp.ScoreUpdate(CurrentScore);
    }

    public int FinalScore()
    {
        return CurrentScore;
    }
}
