using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float MaxEnemy = 4;
    [SerializeField]
    private float CurrentEnemy = 0;

    [SerializeField]
    private GameObject Enemy;

    private bool SpawnDelay = false;


    void Update()
    {
        if (CurrentEnemy < MaxEnemy && SpawnDelay == false)
        {
            SpawnDelay = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        Vector2 SpawnPosition = new Vector2(Random.Range(-2.6f, 2.6f), 4.6f);
        Instantiate(Enemy, SpawnPosition, Quaternion.identity);
        CurrentEnemy++;
        yield return new WaitForSeconds(2f);
        SpawnDelay = false;

    }
}
