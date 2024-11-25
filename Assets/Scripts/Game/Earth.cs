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

    void Start()
    {
        EarthUp.EarthLiveUpdate(EarthLives);
    }

    // cho trai dat quay lien tuc theo truc z
    void Update()
    {
        transform.Rotate(0, 0, 1 * Time.deltaTime, Space.World);
    }

    // kiem tra va cham neu la ke dich nhanh se tru mau va boss thien thach se thua luon
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
