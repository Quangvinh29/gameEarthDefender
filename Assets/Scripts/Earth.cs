using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField]
    private int EarthLives = 3;

    private UIManager EarthUp;
    private SpawnManager Repspawn;

    void Start()
    {
        EarthUp  = GameObject.Find("Canvas").GetComponent<UIManager>();
        Repspawn = GameObject.Find("Spawn").GetComponent<SpawnManager>();

        if (EarthUp == null || Repspawn == null)
        {
            Debug.Log("EarthUp/Respawn is NULL!");
        }
    }

    
    void Update()
    {
        EarthUp.EarthLiveUpdate(EarthLives);

        transform.Rotate(0, 0, 1 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EarthLives--;
            Destroy(other.gameObject);
            Repspawn.Respawn();

        }
    }
}
