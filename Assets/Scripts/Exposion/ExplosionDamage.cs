using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    private PlayerMovement DamagePlayer;

    private void Start()
    {
        DamagePlayer = GameObject.Find("Player").GetComponent<PlayerMovement>();

        Destroy(gameObject, 2.37f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamagePlayer.Damage(3);
        }
    }
}
