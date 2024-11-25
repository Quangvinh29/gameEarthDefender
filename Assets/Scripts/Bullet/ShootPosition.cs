using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPosition : MonoBehaviour
{
    public GameObject EBullet;
    public GameObject[] PBullet;

    private int BulletTier = 1;
    private int ShootTier = 1, MaxShootTier = 3;
    private int MaxTier = 3;

    public bool PlayerShoot = false, EnemyShoot = false;
    public bool QuaAi;

    private AudioSource ShootAudio;

    private Score AddScore;

    private float EnemyShootSpeed; 

    private void Start()
    {
        ShootAudio = GetComponent<AudioSource>();

        AddScore = GameObject.Find("ScoreShow").GetComponent<Score>();

        EnemyShootSpeed = Random.Range(6f, 9f);
    }

    void Update()
    {
        if (EnemyShoot == false && tag == "EnemyShootDirection")
        {
            EnemyShoot = true;
            StartCoroutine(EnemyBanDan());
        }

        if (PlayerShoot == false && tag == "PlayerShootDirection" && QuaAi == false)
        {
            PlayerShoot = true;
            StartCoroutine(PlayerBanDan());
        }
    }

    // player tu dong ban dan
    IEnumerator PlayerBanDan()
    {
        for(int i = 0; i < ShootTier; i++)
        {
            Instantiate(PBullet[BulletTier - 1], transform.position, Quaternion.identity);
            ShootAudio.Play();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1f);
        PlayerShoot = false;
    }

    // ke dic thuong tu dong ban dan
    IEnumerator EnemyBanDan()
    {
        Instantiate(EBullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(EnemyShootSpeed);
        EnemyShoot = false;
    }

    // nang cap dan cho player
    public void UpBullet()
    {
        if (BulletTier < MaxTier)
        {
            BulletTier++;
        }
        else if (BulletTier >= MaxTier)
        {
            BulletTier = MaxTier;
            AddScore.AddScore(50);
        }
    }

    // nang cap ban dan cho player
    public void UpShoot()
    {
        if (ShootTier < MaxShootTier)
        {
            ShootTier++;
        }
        else if (ShootTier >= MaxShootTier)
        {
            ShootTier = MaxShootTier;
            AddScore.AddScore(70);
        }
    }

    // lay kha nang giam ngau nhien
    public float RandomDecreaseUpdate()
    {
        float RandomDecrease = Random.value;
        return RandomDecrease;
    }

    // thuc hien giam nang cap
    public void DecreaseUpdate(float RandomDecrase)
    {

        if (RandomDecrase < 0.4f)
        {
            if (ShootTier > 1)
            {
                ShootTier--;
            }
            else if (BulletTier > 1)
            {
                BulletTier--;
            }

        }
        else if (RandomDecrase < 1f)
        {
            if (BulletTier > 1)
            {
                BulletTier--;
            }
            else if (ShootTier > 1)
            {
                ShootTier--;
            }

        }
    }

    // tam thoi tat kha nang ban cho player khi qua 1 man
    public IEnumerator DaQuaAi()
    {
        QuaAi = true;
        yield return new WaitForSeconds(10.2f);
        QuaAi = false;
    }


}
