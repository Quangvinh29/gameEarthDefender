using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FinalBossBehavior : MonoBehaviour
{
    private float PositionY = 3.15f;
    private Vector2 EnemyMove;

    private int BossOneSpeed = 2, Health;

    private Transform playerPosition;
    private Score AddScore;

    public GameObject Explosion;

    private bool CanUseSkill = false;
    private float[] SkillRate = { 0.2f, 0.4f, 1f };
    private bool[] SkillRecoverYet = { true, true, true };
    private float UseSkillChange = 0.4f;

    private float UseSkillNumber, SkillRateNumber;

    private bool CanBanTenLua = false, DangBanTenLua = false;
    private float ShootTime = 0;
    public GameObject TenLua;

    private bool OnShield = false;
    private int ShieldHealth;
    public GameObject Shield;

    public GameObject DeBoss;
    private int CurrentDeBoss = 0;
    private int MaxDeBoss = 2;

    public bool Dead = false;

    private LargeEnemyShot Shoot;


    void Start()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
        AddScore = GameObject.Find("ScoreShow").GetComponent<Score>();
        Shoot = GetComponentInChildren<LargeEnemyShot>();


        EnemyMove = UnityEngine.Random.Range(0, 2) == 0 ? Vector2.right : Vector2.left;
        Health = UnityEngine.Random.Range(130, 160);

        StartCoroutine(BatDauDiChuyen());
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.y - PositionY) <= 0.5f && DangBanTenLua == false)
        {
            DiChuyenQuaLai();
        }

        // kiem tra dieu kien neu co the dung ki nang, thuc hien chon ki nang
        if (CanUseSkill == true)
        {
            ChonSkill();

        }

        // kiem tra dieu kien neu CanBanTenLua == true, kien tra vi tri co the va dan ten lua.
        if (CanBanTenLua == true)
        {
            if (Mathf.Abs(playerPosition.position.x - transform.position.x) < 1f)
            {
                DangBanTenLua = true;
                StartCoroutine(BanTenLua());
            }
        }
    }

    IEnumerator BatDauDiChuyen()
    {
        while (transform.position.y > PositionY)
        {
            transform.Translate(Vector2.down * 1 * Time.deltaTime);
            yield return null;
        }

        Shoot.enabled = true;
        StartCoroutine(UseSkill());
    }

    private void DiChuyenQuaLai()
    {
        transform.Translate(EnemyMove * BossOneSpeed * Time.deltaTime);

        if (transform.position.x <= -1.80f)
        {
            transform.position = new Vector2(-1.80f, transform.position.y);
            EnemyMove = -EnemyMove;
        }
        else if (transform.position.x >= 1.80f)
        {
            transform.position = new Vector2(1.80f, transform.position.y);
            EnemyMove = -EnemyMove;
        }
    }

    // ham use skill, goi 1 lan khi di chuyen xong de thuc hien vong lap dung ki nang
    IEnumerator UseSkill()
    {
        while (Dead == false)
        {
            UseSkillNumber = UnityEngine.Random.value;
            if (UseSkillNumber <= UseSkillChange)
            {
                CanUseSkill = true;

            }
            yield return new WaitForSeconds(4f);
        }
    }

    // thuc hien chon ki nang neu dieu kien CanUseSkill = true va thuc hien chon ki nang se dung
    private void ChonSkill()
    {
        SkillRateNumber = UnityEngine.Random.value;
        for (int i = 0; i < SkillRate.Length; i++)
        {
            if (SkillRateNumber < SkillRate[i] && SkillRecoverYet[i] == true)
            {
                int Skill = i;
                PhatDongKyNang(Skill);
                break;

            }
        }
    }

    // kiem tra loai ki nang se phat dong
    private void PhatDongKyNang(int Skill)
    {
        if (Skill == 0)
        {
            CanBanTenLua = true;
        }
        else if (Skill == 1)
        {
            StartCoroutine(ActiveShield());
        }
        else if (Skill == 2)
        {
            if (CurrentDeBoss < MaxDeBoss)
            {
                StartCoroutine(SpawnDeBoss());
            }
        }

        CanUseSkill = false;
    }

    // ham thuc hien ki nang ban ten lua
    IEnumerator BanTenLua()
    {
        Shoot.enabled = false;
        CanBanTenLua = false;
        SkillRecoverYet[0] = false;

        Vector2 PositionSpawn = new Vector2(transform.position.x, (transform.position.y - 1.79f));
        Instantiate(TenLua, PositionSpawn, Quaternion.identity);

        while (ShootTime < 3)
        {
            ShootTime += Time.deltaTime;
            yield return null;
        }
        DangBanTenLua = false;

        Shoot.enabled = true;
        yield return new WaitForSeconds(40f);
        SkillRecoverYet[0] = true;
    }

    // thuc hien ki nang kich hoat shield
    IEnumerator ActiveShield()
    {
        SkillRecoverYet[1] = false;

        OnShield = true;
        ShieldHealth = UnityEngine.Random.Range(30, 40);
        Shield.SetActive(true);

        if (OnShield == false)
        {
            yield return new WaitForSeconds(15f);
            SkillRecoverYet[1] = true;
        }
    }

    // thuc hien ki nang spawn deBoss
    IEnumerator SpawnDeBoss()
    {
        SkillRecoverYet[2] = false;

        for (int x = CurrentDeBoss; x < MaxDeBoss; x++)
        {
            Vector2 Postion = UnityEngine.Random.Range(0, 2) == 0 ? new Vector2(1.88f, 6.27f) : new Vector2(-1.88f, 6.27f);
            Instantiate(DeBoss, Postion, Quaternion.identity);
            CurrentDeBoss++;
            yield return null;
        }

        yield return new WaitForSeconds(20f);
        SkillRecoverYet[2] = true;
    }

    // kiem tra DeBoss neu chet se spawn moi
    public void DeBossDead()
    {
        CurrentDeBoss--;
    }

    // kiem tra nhan sat thuong, khi chet se thuc hien tieu diet tat ca ke dich va win game.
    public void GetDamage(int Damage)
    {
        if (OnShield == false)
        {
            Health -= Damage;
        }
        else if (OnShield == true)
        {
            ShieldHealth -= Damage;
        }

        if (ShieldHealth <= 0)
        {
            Shield.SetActive(false);
        }

        if (Health <= 0 || Dead == true)
        {
            Dead = true;
            AddScore.AddScore(500);
            Instantiate(Explosion, transform.position, Quaternion.identity);

            GameObject[] DeBoss = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in DeBoss)
            {
                Destroy(enemy);  
            }

            WinAndLose ShowMenu = GameObject.Find("WinAndLoseGame").GetComponent<WinAndLose>();
            ShowMenu.StartCoroutine(ShowMenu.WinGame());
            Destroy(gameObject);

        }
    }

}
