using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

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

        if (transform.position.y < -4.8f)
        {
            Destroy(gameObject);
            Rspawn.Respawn();
        }
    }

     // neu nhan sat thuong tu dan thi chet va cong diem
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Rspawn.Respawn();
            AScore.AddScore();
        }
    }

}
