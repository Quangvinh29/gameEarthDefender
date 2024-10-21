using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject Bullet;
    public bool PlayerShoot = false, EnemyShoot = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && PlayerShoot == false && tag == "PlayerShootDirection")
        {
            PlayerShoot = true;
            StartCoroutine(PlayerBanDan());
        }

        if (EnemyShoot == false && tag == "EnemyShootDirection")
        {
            EnemyShoot = true;
            StartCoroutine(EnemyBanDan());
        }
      
    }

    IEnumerator PlayerBanDan()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        PlayerShoot = false;
    }

    IEnumerator EnemyBanDan()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        EnemyShoot = false;
    }
}
