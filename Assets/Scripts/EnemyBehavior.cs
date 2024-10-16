using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private int speed = 2;
    private bool IsShoot = false;
    [SerializeField]
    private GameObject EnemyBullet;

    void Update()
    {
        DiChuyen();
        if(IsShoot == false)
        {
            IsShoot = true;
            StartCoroutine(BanDan());
        }
    }

    public void DiChuyen()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    IEnumerator BanDan()
    {
        Instantiate(EnemyBullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        IsShoot=false;
    }
}
