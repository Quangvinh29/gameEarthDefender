using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField]
    private GameObject Explosion;

    private int speed = 2;
    private SpawnManager Rspawn;
    private Score AScore;

    // ham start tham goi tham chieu den Spawn
    void Start()
    {
         Rspawn = GameObject.Find("Spawn").GetComponent<SpawnManager>();
        if (Rspawn == null )
        {
            Debug.Log("Rspawn is NULL!");
        }

        AScore = GameObject.Find("ScoreShow").GetComponent<Score>();
        if (AScore == null)
        {
            Debug.Log("ACore is NULL!");
        }
    }

    
    void Update()
    {
        DiChuyen();
    }

    //ham di chuyen va neu vuot qua -4.8f thi se chet
    public void DiChuyen()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    // neu nhan sat thuong tu dan thi chet va cong diem
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Rspawn.Respawn();
            AScore.AddScore();
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }

}
