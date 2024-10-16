using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject Bullet;
    public bool isShoot = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isShoot == false)
        {
            isShoot = true;
            StartCoroutine(BanDan());
        }
      
    }

    IEnumerator BanDan()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        isShoot = false;
    }
}
