
using System.Collections;
using System.Linq;
using TMPro;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.Localization;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public float MaxEnemy = 4;
    [SerializeField]
    private float CurrentEnemy = 0;

    [SerializeField]
    private GameObject[] Enemy;

    private float[] SpawnRate = { -0.1f, 0f, 0.3f, 1f };
    private float SpawnNumber;

    private bool CanSpawn = true;
    private float spawnDelay = 3f;

    [SerializeField]
    private int EnemyKill = 0;
    private int[] SpawnTier;
    private int CurrentTier = 0;

    public GameObject[] Boss;

    private bool isSpawning = false; 

    private bool BossSpawned = false;

    public int CheckSpawn;

    public Image ThongBaoQA;
    public LocalizedString ThongBao1;
    public LocalizedString DotTiep1;
    public LocalizedString ThongBao2;
    public LocalizedString DotTiep2;

    public TMP_Text ThongBaoText;
    public TMP_Text DotSauText;

    // thiet lap cac moc tang do kho bang Ramdom.Range mot cach ngau nhien
    private void Start()
    {
  
        SpawnTier = new int[] { UnityEngine.Random.Range(40, 60), UnityEngine.Random.Range(90, 130), UnityEngine.Random.Range(150, 170)};
        CheckSpawn = SpawnTier[CurrentTier];
    }

    // update thuc hien spawn ke dich va den mot muc do nhat dinh, spawn boss
    void Update()
    {
        if (CanSpawn == true && isSpawning == false && CurrentEnemy < MaxEnemy)
        {
            StartCoroutine(SpawnEnemyWithDelay());
        }

        if (CurrentTier < SpawnTier.Length && EnemyKill >= SpawnTier[CurrentTier])
        {
            if(BossSpawned == false)
            {
                SpawnBoss();
            }
        }
    }

    // thuc hien spawn ke dich voi thoi gian delay = spawnDelay
    IEnumerator SpawnEnemyWithDelay()
    {
        isSpawning = true; 
        yield return new WaitForSeconds(spawnDelay);
        SpawnEnemy();
        isSpawning = false; 
    }

    // ham lay vi tri, lay loai spawn va spawn
    public void SpawnEnemy()
    {
        Vector2 SpawnPosition = new Vector2(UnityEngine.Random.Range(-2f, 2f), transform.position.y);
        SpawnNumber = UnityEngine.Random.value;

  
        for (int i = 0; i < Enemy.Length; i++)
        {
            if (SpawnNumber <= SpawnRate[i])
            {
                CurrentEnemy++;
                Instantiate(Enemy[i], SpawnPosition, Quaternion.identity);
                break;
            }
        }
    }

    // ham Respawn goi 1 lan khi mot ke dich chet
    public void Respawn()
    {
        if(CurrentEnemy <= 0)
        {
            CurrentEnemy = 0;
        }
        else
        {
            CurrentEnemy--;
        }
        EnemyKill++;
    }

    // ham tang do kho bang cach tang spawnDelay, Max so luong va ty le spawn của cac ke dich kho hon
    private void IncreaseDifficulty(int CurrentTier)
    {
        if(CurrentTier == 1)
        {
            spawnDelay = 2.5f;
            MaxEnemy = 5;
            SpawnRate[0] += 0.15f;
            SpawnRate[1] += 0.2f;
            SpawnRate[2] += 0.1f;
        }
        if (CurrentTier == 2)
        {
            spawnDelay = 1.5f;
            MaxEnemy = 6;
            SpawnRate[0] += 0.1f;
            SpawnRate[1] += 0.15f;
            SpawnRate[2] += 0.2f;
 
         }
    }

    // ham spawn boss goi mot lan trong update khi dat du dieu kien
    private void SpawnBoss()
    {
        BossSpawned = true;
        if (CurrentTier < Boss.Length)
        {
            CanSpawn = false;
            Instantiate(Boss[CurrentTier], new Vector2(0, transform.position.y), Quaternion.identity);
        }
    }

    // ham boss di danh bai goi 1 lan khi boss da chet de hien thi thong bao qua man va tang do kho
    public IEnumerator BossDefended()
    {

        ThongBaoQA.gameObject.SetActive(true);

        switch (CurrentTier)
        {
            case 0:
                ThongBao1.StringChanged += UpdateThongBao;
                DotTiep1.StringChanged += UpdateQuaAi;

                ThongBao1.RefreshString();
                DotTiep1.RefreshString();
                break;
            case 1:
                ThongBao2.StringChanged += UpdateThongBao;
                DotTiep2.StringChanged += UpdateQuaAi;

                ThongBao2.RefreshString();
                DotTiep2.RefreshString();
                break;
        }

        ShootPosition[] playerGun = GameObject.FindGameObjectsWithTag("PlayerShootDirection").Select(go => go.GetComponent<ShootPosition>()).ToArray();
        foreach(ShootPosition wait in playerGun)
        {
            wait.StartCoroutine(wait.DaQuaAi());
        }


        float ThongBaoTime = 10f;
        float blinktime = 0.5f;

        while (ThongBaoTime > 0)
        {
            Color color = DotSauText.color;

            color.a = 0f;
            DotSauText.color = color; 
            yield return new WaitForSeconds(blinktime); 


            color.a = 1f;
            DotSauText.color = color; 
            yield return new WaitForSeconds(blinktime);

            ThongBaoTime -= 2 * blinktime;
        }

        ThongBaoQA.gameObject.SetActive(false);
        CanSpawn = true;
        BossSpawned = false;
        CurrentTier += 1;
        IncreaseDifficulty(CurrentTier);

        if (CurrentTier < SpawnTier.Length)
        {
            CheckSpawn = SpawnTier[CurrentTier];
        }
    }

        
    // ham thuc hien thong bao ap dung localization
    private void UpdateThongBao(string value)
    {
        ThongBaoText.text = value;
    }
    private void UpdateQuaAi(string value)
    {
        DotSauText.text = value;
    }
}
