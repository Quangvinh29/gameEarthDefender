using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private int CurrentScore = 0;

    private int ScorePerEnemy = 10;

    private UIManager ScoreUp;

    private void Start()
    {
        ScoreUp = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (ScoreUp == null)
        {
            Debug.Log("ScoreUp is Null!");
        }
    }

    public void AddScore()
    {
        CurrentScore += ScorePerEnemy;
    }

    private void Update()
    {
        ScoreUp.ScoreUpdate(CurrentScore);
    }
}
