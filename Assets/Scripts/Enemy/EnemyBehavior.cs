using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
 
    public GameObject Explosion;

    public int EnemyHealth, EnemyScore, DamDamage;
    public float EnemySpeed;
    public float DropChange;

    private SpawnManager Rspawn;
    private Score AScore;
    private DropPowerUp Drop;
    private PlayerMovement player;

    private float randomY;
    private Vector2 EnemyMove;

    private float MoveRate = 0.75f, MoveTypeNumber;


    void Start()
    {
        Rspawn = GameObject.Find("Spawn").GetComponent<SpawnManager>();
        if (Rspawn == null)
        {
            Debug.Log("Rspawn is NULL!");
        }

        AScore = GameObject.Find("ScoreShow").GetComponent<Score>();
        if (AScore == null)
        {
            Debug.Log("ACore is NULL!");
        }

        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if (player == null)
        {
            Debug.Log("player is NULL!");
        }

        Drop = GetComponent<DropPowerUp>();
        if (Drop == null)
        {
            Debug.Log(" Drop is NULL!");
        }


        randomY = UnityEngine.Random.Range(0f, 3f);

        MoveTypeNumber = UnityEngine.Random.value;
        if (MoveTypeNumber <= MoveRate)
        {
            EnemyMove = UnityEngine.Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;
        }

        CaiDatMauVaDiem();
        StartCoroutine(BatDauDiChuyen());
    }


    void Update()
    {
        if (MoveTypeNumber <= MoveRate) 
        {
            DiChuyenQuaLai();
        }
    }

    private void CaiDatMauVaDiem()
    {
        if (gameObject.name == "LargeEnemy(Clone)")
        {
           EnemyHealth = UnityEngine.Random.Range(10, 16);
            EnemyScore = UnityEngine.Random.Range(60, 80);
        }

        if (gameObject.name == "MediumEnemy(Clone)")
        {
            EnemyHealth = UnityEngine.Random.Range(5, 8);
            EnemyScore = UnityEngine.Random.Range(30, 50);

        }

        if (gameObject.name == "SmallEnemy(Clone)")
        {
            EnemyHealth = UnityEngine.Random.Range(2, 4);
            EnemyScore = UnityEngine.Random.Range(10, 30);

        }
    }

    IEnumerator BatDauDiChuyen()
    {
        while (transform.position.y > randomY)
        {
            transform.Translate(Vector2.down * 4 * Time.deltaTime);
            yield return null;
        }
    }

    private void DiChuyenQuaLai()
    {
        transform.Translate(EnemyMove * EnemySpeed * Time.deltaTime);

        if (transform.position.x <= -2f)
        {
            transform.position = new Vector2(-2f, transform.position.y);
            EnemyMove = -EnemyMove;
        }
        else if (transform.position.x >= 2f)
        {
            transform.position = new Vector2(2f, transform.position.y);
            EnemyMove = -EnemyMove;
        }
    }

    // neu nhan sat thuong tu dan thi chet va cong diem
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            player.Damage(DamDamage);
            Dead();
        }
    }

    public void TakeDamage(int Damage)
    {
        EnemyHealth -= Damage;

        if(EnemyHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
        Rspawn.Respawn();
        AScore.AddScore(EnemyScore);
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Drop.DropPU(DropChange);
    }
}
