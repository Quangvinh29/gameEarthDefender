using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float speed = 3;
   
    void Update()
    {
      
        transform.Translate(Vector2.down * speed * Time.deltaTime);
  

        if (transform.position.y < -4.8f)
        {
            Destroy(gameObject);
        }
    }
}
