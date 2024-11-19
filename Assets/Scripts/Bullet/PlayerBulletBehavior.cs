using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    public float speed = 5f;
    public int Damage;

    private EnemyBehavior DamageEnemy;
    private BossOneBehavior DamageBossOne;
    private BossAsteroidBehavior DamageBossAsteroid;
    private FinalBossBehavior DamageFinalBoss;
    private DeBossBehavior DamageDeBoss;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if(transform.position.y > 4.8f)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.name != "DeBoss(Clone)")
        {
            DamageEnemy = other.GetComponent<EnemyBehavior>();
            DamageEnemy.TakeDamage(Damage);
            Destroy(gameObject);
        }

        if (other.gameObject.name == "Boss 1(Clone)")
        {
            DamageBossOne = other.GetComponent<BossOneBehavior>();
            DamageBossOne.GetDamage(Damage);
            Destroy(gameObject);
        }

        if (other.gameObject.name == "BigAsteroid(Clone)")
        {
            DamageBossAsteroid = other.GetComponent<BossAsteroidBehavior>();
            DamageBossAsteroid.GetDamage(Damage);
            Destroy(gameObject);
        }

        if (other.gameObject.name == "FinalBoss(Clone)")
        {
            DamageFinalBoss = other.GetComponent<FinalBossBehavior>();
            DamageFinalBoss.GetDamage(Damage);
            Destroy(gameObject);
        }

        if (other.gameObject.name == "DeBoss(Clone)")
        {
            DamageDeBoss = other.GetComponent<DeBossBehavior>();
            DamageDeBoss.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
