using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int CurrentLives = 3, MaxCurrentLives = 3, MaxLives = 6;

    [SerializeField]
    private GameObject Explosion, Shield;

    private UIManager HealthUp;

    private Score AddScore;

    private bool HaveShield = false;

    private bool UnDeadOn = false;
    private SpriteRenderer spriteRenderer;

    private ShootPosition[] DecreaseUpdate;
    private bool NhanSatThuong = false;

    public GameObject FinishMenu;
    public TMP_Text Score;

    public FixedJoystick joystick;

    public bool DiChuyenXong = false;

    public GameObject Gun;

    private int healthPerlives = 3;
    private int shieldHealth;

    private void Start()
    {
        HealthUp = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(HealthUp == null)
        {
            Debug.Log("HealthUI is Null!");
        }

        AddScore = GameObject.Find("ScoreShow").GetComponent<Score>();
        if (AddScore == null)
        {
            Debug.Log(" AddScore is Null!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.Log("spriteRenderer is Null!");
        }

        StartCoroutine(BatDauDiChuyen());
    }

    // ham update() thuc thi di chuyen
    void Update()
    { 
         DiChuyen();
    }

    // ham Bat dau di chuyen, den vi tri chi dinh thi moi goi kha nang ban de bat dau tu dong ban
    IEnumerator BatDauDiChuyen()
    {
        while(transform.position.y < -2.5f)
        {
            transform.Translate(Vector2.up * 2 * Time.deltaTime);
            yield return null;
        }
        Gun.SetActive(true);

        DecreaseUpdate = GameObject.FindGameObjectsWithTag("PlayerShootDirection").Select(go => go.GetComponent<ShootPosition>()).ToArray();
        if (DecreaseUpdate == null)
        {
            Debug.Log("DecreaseUpdate is Null!");
        }

        DiChuyenXong = true;
    }

    // ham tinh toan di chuyen
    public void DiChuyen()
    {
        Vector2 VectorDiChuyen = new Vector2(joystick.Horizontal, joystick.Vertical);
        transform.Translate(VectorDiChuyen * 3.5f * Time.deltaTime);

        if(DiChuyenXong == true)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2f, 2f), Mathf.Clamp(transform.position.y, -4.8f, 4.8f));
        }
    }

    // kiem tra va cham voi dan cua ke dich nhan sat thuong
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Damage(1);
            Destroy(other.gameObject);
        }
    }

    // ham damage tinh toan luong sat thuong nhan vao cho moi nguon
    public void Damage(int TakeDamage)
    {
        
        if (HaveShield == false && UnDeadOn == false && NhanSatThuong == false)
        {
            healthPerlives -= TakeDamage;
            if(healthPerlives == 0)
            {
                TruMau();
                healthPerlives = 3;
            }
            else if(healthPerlives < 0)
            {
                int DamageLeft = TakeDamage - 3;
                TruMau();
                healthPerlives = 3;
                Damage(DamageLeft);
            }
        }
        else if (HaveShield == true)
        {
            if(shieldHealth > 0)
            {
                shieldHealth -= TakeDamage;
            }

            if( shieldHealth <= 0)
            {
                    HaveShield = false;
                    Shield.SetActive(false);
            }
        }

        if (CurrentLives <= 0)
        {
            Dead();
        }
    }

    // ham thuc hien tru mau tren UI va giam mot nang cap ngau nhien
    public void TruMau()
    {
        NhanSatThuong = true;
        StartCoroutine(TranhSatThuong());

        HealthUp.HealthUpdate(CurrentLives);
        CurrentLives--;
        float randomDecrease = Random.value;
        foreach (ShootPosition shootPosition in DecreaseUpdate)
        {
            shootPosition.DecreaseUpdate(randomDecrease);
        }
    }

    // sau khi bi tru mau se mien nhiem 1 giay
    IEnumerator TranhSatThuong()
    {
        yield return new WaitForSeconds(1f);
        NhanSatThuong = false;
    }
    
    // khi player bi tieu diet, tat kha nang ban dan, tang hinh player va goi thua game theo player chet
    public void Dead()
    {
        Gun.SetActive(false);   
        spriteRenderer.enabled = false;
       WinAndLose GameOver = GameObject.Find("WinAndLoseGame").GetComponent<WinAndLose>();
        GameOver.StartCoroutine(GameOver.PlayerDeathGameOver());
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }

    // ham duoc goi khi nhan power up nhan shield, kich hoat shield
    public void ShieldOn()
    {
        HaveShield = true;
        Shield.SetActive(true);
        shieldHealth = Random.Range(3, 6);
    }

    // ham duoc goi khi nhan power up bat tu trong 15 giay, thay doi mau sac de hien thi 
    public IEnumerator PowerUpUnDead()
    {
        UnDeadOn = true;
        Color UndeadColor = new Color(1f, 0.65f, 0f);
        Color OriginColor = spriteRenderer.color;

        spriteRenderer.color = UndeadColor;
        yield return new WaitForSeconds(15f);

        spriteRenderer.color = OriginColor;
        UnDeadOn = false;
    }

    // ham duoc goi khi nhan power up hoi mau, hoi 1 mau va neu da day mau, cong 30 diem
    public void HoiMau()
    {
        if(CurrentLives < MaxCurrentLives)
        {
            CurrentLives++;
            HealthUp.PowerUpGetHealth(CurrentLives);
;        }
        else if (CurrentLives >= MaxCurrentLives)
        {
            AddScore.AddScore(30);
        }
    }

    // ham duoc goi khi nhan power up tang 1 mau toi da, tang 1 mau toi da va neu da day mau, thuc hien hoi mau
    public void AddLives()
    {
        if(MaxCurrentLives < MaxLives)
        {
            if(CurrentLives == MaxCurrentLives)
            {
                CurrentLives++;
            }
            MaxCurrentLives++;
            HealthUp.AddHealth(CurrentLives, MaxCurrentLives);
        }

        if(MaxCurrentLives >= MaxLives)
        {
            HoiMau();
        }
    }

}
