using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyScript : MonoBehaviour
{
    private int speed = 6, Damage = 2;

    [SerializeField]
    private GameObject Explosion;

    private PlayerMovement player;
    private SpawnManager RSpawn;
    private Score AddScore;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        RSpawn = GameObject.Find("Spawn").GetComponent<SpawnManager>();
        AddScore = GameObject.Find("ScoreShow").GetComponent<Score>();
    }
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Dead();
            player.Damage(Damage);
        }

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Dead();
            AddScore.AddScore(20);
        }
    }

    private void Dead()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        RSpawn.Respawn();
    }
}
