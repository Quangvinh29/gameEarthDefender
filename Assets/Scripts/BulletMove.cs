using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        if (tag == "PlayerBullet")
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (tag == "EnemyBullet")
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        if(transform.position.y < -4.8f  || transform.position.y > 4.8f)
        {
            Destroy(gameObject);
        }

    }
}
