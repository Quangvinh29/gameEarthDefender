using System;
using System.Collections;
using UnityEngine;

public class BossOneBehavior : MonoBehaviour
{
    private float PositionY = 3.15f;
    private Vector2 EnemyMove;

    private int BossOneSpeed = 2, Health;

    private Transform playerPosition;
    private LargeEnemyShot ShootDirection;
    private Score AddScore;
    private PlayerMovement player;
    private SpawnManager Spawn;

    public GameObject Explosion;

    private bool CanDashAttack = false;
    public bool Dead = false;

    // thuc hien goi cac tham chieu can thiet va cai dat mau, cach di chuyen
    void Start()
    {
        ShootDirection = GetComponentInChildren<LargeEnemyShot>();
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
        AddScore = GameObject.Find("ScoreShow").GetComponent<Score>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Spawn = GameObject.Find("Spawn").GetComponent<SpawnManager>();

        EnemyMove = UnityEngine.Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;
        Health = UnityEngine.Random.Range(50, 80);

        StartCoroutine(BatDauDiChuyen());
    }

    // thuc hien di chuyen trai phai, neu CanDashAttack == true va vi tri dung, thuc hien ki nang dash
    void Update()
    {
        if (Mathf.Abs(transform.position.y - PositionY) <= 0.5f)
        {
            DiChuyenQuaLai();
        }

        if(CanDashAttack == true) 
        {
            if (Mathf.Abs(playerPosition.position.x - transform.position.x) < 1f)
            {
                 StartCoroutine(DashAttack());
            }
        } 
    }

    // ham bat dau di chuyen, de vi tri chi dinh moi di chuyen qua lai
    IEnumerator BatDauDiChuyen()
    {
        while (transform.position.y > PositionY)
        {
            transform.Translate(Vector2.down * 1 * Time.deltaTime);
            yield return null;
        }
        CanDashAttack = true;
    }

    // ham di chuyen trai phai, den vi tri cham goc man hinh thi quay lai
    private void DiChuyenQuaLai()
    {
        transform.Translate(EnemyMove * BossOneSpeed * Time.deltaTime);

        if (transform.position.x <= -1.80f)
        {
            transform.position = new Vector2(-1.80f, transform.position.y);
            EnemyMove = -EnemyMove;
        }
        else if (transform.position.x >= 1.80f)
        {
            transform.position = new Vector2(1.80f, transform.position.y);
            EnemyMove = -EnemyMove;
        }
    }

    // thuc hien ki nang dash sau do lui lai
    IEnumerator DashAttack()
    {
        Vector2 OriginPostion = transform.position;
        Vector2 TargetPosition = new Vector2(transform.position.x, -3.4f);

        CanDashAttack = false;
        ShootDirection.enabled = false;
        while (transform.position.y > TargetPosition.y)
        {
            transform.Translate(Vector2.down * 4.7f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.7f);

        while (transform.position.y < OriginPostion.y)
        {
            transform.Translate(Vector2.up * 3 * Time.deltaTime);
            yield return null;
        }
        ShootDirection.enabled = true;

        yield return new WaitForSeconds(7f);
        CanDashAttack = true;
    }

    // ham nhan sat thuong khi trung dan player
    public void GetDamage(int Damage)
    {
        Health -= Damage;

        if(Health <= 0 || Dead == true)
        {
            Destroy(gameObject);
            AddScore.AddScore(200);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Spawn.StartCoroutine(Spawn.BossDefended());
        }
    }

    // va cham voi player se tru 3 mau player
    private void OnTriggerEnter2D(Collider2D cother)
    {
        if(cother.gameObject.CompareTag("Player"))
        {
            player.Damage(3);
        }
    }
}
