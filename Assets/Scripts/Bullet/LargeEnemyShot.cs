using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemyShot : MonoBehaviour
{
    public GameObject Bullet;

    private bool LargeEnemyShoot = false;
    private float EnemyShootSpeed;


    private void Start()
    {
       EnemyShootSpeed = Random.Range(9f, 13f);
    }

    private void Update()
    {
        if (LargeEnemyShoot == false)
        {
            LargeEnemyShoot = true;
            StartCoroutine(LargeEnemyBanDan());
        }
    }

    IEnumerator LargeEnemyBanDan()
    {
        for(int i = 0; i < 3; i++)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(EnemyShootSpeed);
        LargeEnemyShoot = false;
    }
}
