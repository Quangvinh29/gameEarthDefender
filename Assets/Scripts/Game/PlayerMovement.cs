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

    // ham tinh toan chuyen dong 
    public void DiChuyen()
    {
        // Di chuyen bang nut bam
        Vector2 VectorDiChuyen = new Vector2(joystick.Horizontal, joystick.Vertical);
        transform.Translate(VectorDiChuyen * 3.5f * Time.deltaTime);

        if(DiChuyenXong == true)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2f, 2f), Mathf.Clamp(transform.position.y, -4.8f, 4.8f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Damage(1);
            Destroy(other.gameObject);
        }
    }

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


    IEnumerator TranhSatThuong()
    {
        yield return new WaitForSeconds(0.4f);
        NhanSatThuong = false;
    }
    

    public void Dead()
    {
        Gun.SetActive(false);   
        spriteRenderer.enabled = false;
       WinAndLose GameOver = GameObject.Find("WinAndLoseGame").GetComponent<WinAndLose>();
        GameOver.StartCoroutine(GameOver.PlayerDeathGameOver());
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }

    public void ShieldOn()
    {
        HaveShield = true;
        Shield.SetActive(true);
        shieldHealth = Random.Range(3, 6);
    }

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
