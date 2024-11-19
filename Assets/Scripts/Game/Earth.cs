using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Earth : MonoBehaviour
{
    [SerializeField]
    private int EarthLives = 3;

    private UIManager EarthUp;
    private SpawnManager Repspawn;

    public GameObject FinishMenu;
    public TMP_Text MoTa;
    public TMP_Text HienThiDiem;
    private Score Score;

    void Start()
    {
        EarthUp  = GameObject.Find("Canvas").GetComponent<UIManager>();
        Repspawn = GameObject.Find("Spawn").GetComponent<SpawnManager>();
        Score = GameObject.Find("ScoreShow").GetComponent<Score>();

        EarthUp.EarthLiveUpdate(EarthLives);
    }

    
    void Update()
    {
        transform.Rotate(0, 0, 1 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FastEnemy")
        {
            EarthLives--;
            EarthUp.EarthLiveUpdate(EarthLives);
            Destroy(other.gameObject);
            Repspawn.Respawn();

        }
        if(other.name == "BigAsteroid(Clone)")
        {
            EarthLives-=3;
        }

        if(EarthLives <= 0)
        {
            WinAndLose GameOver = GameObject.Find("WinAndLoseGame").GetComponent<WinAndLose>();
            GameOver.EarthGameOver();
        }
    }

}
