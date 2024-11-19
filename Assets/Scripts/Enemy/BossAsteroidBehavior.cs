using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossAsteroidBehavior : MonoBehaviour
{
    private float Speed = 0.15f, Health;
    public GameObject Explosion;

    private Score AddScore;
    private SpawnManager Spawn;
    public bool Dead = false;

    void Start()
    {
        AddScore = GameObject.Find("ScoreShow").GetComponent<Score>();
        Spawn = GameObject.Find("Spawn").GetComponent<SpawnManager>();

        Health = UnityEngine.Random.Range(80, 120);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0,0,1) * 10 * Time.deltaTime);

        transform.Translate(Vector3.down * Speed * Time.deltaTime, Space.World);
    }

    public void GetDamage(int Damage)
    {
        Health -= Damage;

        if(Health <= 0 || Dead == true) 
        {
            Destroy(gameObject);
            AddScore.AddScore(10);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Spawn.StartCoroutine(Spawn.BossDefended());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }

}
