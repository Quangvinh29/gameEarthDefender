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


    // update thuc hien nhiem vu spawn
    void Update()
    {
        if (CurrentEnemy < MaxEnemy && SpawnDelay == false)
        {
            SpawnDelay = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    // ham spawn ke dich va cooldown
    IEnumerator SpawnEnemy()
    {
        Vector2 SpawnPosition = new Vector2(Random.Range(-2.6f, 2.6f), 4.6f);
        Instantiate(Enemy, SpawnPosition, Quaternion.identity);
        CurrentEnemy++;
        yield return new WaitForSeconds(2f);
        SpawnDelay = false;
    }

    // ham thuc hien respawn
    public void Respawn()
    {
        CurrentEnemy--;
    }
}
