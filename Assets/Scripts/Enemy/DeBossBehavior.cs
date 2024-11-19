using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBossBehavior : MonoBehaviour
{
    public GameObject Explosion;

    public int EnemyHealth,DamDamage;
    public float DropChange, EnemySpeed;

    private DropPowerUp Drop;
    private FinalBossBehavior DeadCount;

    private PlayerMovement player;

    private Vector2 EnemyMove;

    void Start()
    {

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

        DeadCount = GameObject.Find("FinalBoss(Clone)").GetComponent<FinalBossBehavior>();
        if(DeadCount == null)
        {
            Debug.Log(" DeadCount is NULL!");
        }


        EnemyHealth = Random.Range(15, 20);

        EnemyMove = Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;

        StartCoroutine(BatDauDiChuyen());
    }


    void Update()
    {
        if(Mathf.Abs(transform.position.y - 0.5f) < 0.2f)
        {
            DiChuyenQuaLai();
        }     
    }


    IEnumerator BatDauDiChuyen()
    {
        while (transform.position.y > 0.5)
        {
            transform.Translate(Vector2.down * 4 * Time.deltaTime);
            yield return null;
        }
    }

    private void DiChuyenQuaLai()
    {
        transform.Translate(EnemyMove * EnemySpeed * Time.deltaTime);

        if (transform.position.x <= -2.6f)
        {
            transform.position = new Vector2(-2.6f, transform.position.y);
            EnemyMove = -EnemyMove;
        }
        else if (transform.position.x >= 2.6f)
        {
            transform.position = new Vector2(2.6f, transform.position.y);
            EnemyMove = -EnemyMove;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            player.Damage(DamDamage);
            Dead();
        }
    }

    public void TakeDamage(int Damage)
    {
        EnemyHealth -= Damage;

        if (EnemyHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        DeadCount.DeBossDead();
        Destroy(gameObject);
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Drop.DropPU(DropChange);
    }
}
