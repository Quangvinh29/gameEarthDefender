using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TenLuaBehavior : MonoBehaviour
{
    public GameObject Explosion;

    private void Start()
    {
        StartCoroutine(Ban());
    }

    IEnumerator Ban()
    {
        yield return new WaitForSeconds(3f);

        while (transform.position.y > -3.5)
        {
            transform.Translate(Vector2.down * 8f * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }

}


