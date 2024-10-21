using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private int CurrentScore = 0;

    private int ScorePerEnemy = 10;


    public void AddScore()
    {
        CurrentScore += ScorePerEnemy;
    }
}
