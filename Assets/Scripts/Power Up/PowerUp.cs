using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUp : MonoBehaviour
{
    private BoxCollider2D playerCollider;  
    private BoxCollider PowerUpcollider;

    private Score AddScore;
    private PlayerMovement Player;
    private ShootPosition[] UpPowerUp;

    // thuc hien goi cac tham chieu
    private void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        PowerUpcollider = GetComponent<BoxCollider>();

        AddScore = GameObject.Find("ScoreShow").GetComponent<Score>();
        Player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        UpPowerUp = GameObject.FindGameObjectsWithTag("PlayerShootDirection").Select(go => go.GetComponent<ShootPosition>()).ToArray();
                            
                               

        if (UpPowerUp == null)
        {
            Debug.Log("UpPowerUp is null!");
        }
    }

    // khi mot power up duoc spawn, kiem tra co va cham voi player khong (vi power up la doi tuong 3d)
    void Update()
    {
        Bounds playerBounds = playerCollider.bounds;
        Bounds PowerUpBounds = PowerUpcollider.bounds;

   
        if (playerBounds.Intersects(PowerUpBounds))
        {
            RunPowerUp();
            Destroy(gameObject); 
        }


        transform.Translate(Vector3.down * 2 * Time.deltaTime);

        if(transform.position.y < -4.8f)
        {
            Destroy(gameObject);
        }
    }

    // thuc hien ap dung nang luc theo ten cua power up do
    private void RunPowerUp()
    {
        if(gameObject.name == "ScoreBonus(Clone)")
        {
            AddScore.AddScore(Random.Range(10, 100));
        }

        if (gameObject.name == "AddShield(Clone)")
        {
            Player.ShieldOn();
        }

        if(gameObject.name == "Undead(Clone)")
        {
            Player.StartCoroutine(Player.PowerUpUnDead());
        }

        if(gameObject.name == "UpdateBullet(Clone)")
        {
            foreach (ShootPosition shootPosition in UpPowerUp)
            {
                shootPosition.UpBullet();
            }
        }

        if( gameObject.name == "UpdateShoot(Clone)")
        {
            foreach (ShootPosition shootPosition in UpPowerUp)
            {
                shootPosition.UpShoot();
            }
        }

        if(gameObject.name == "HealLives(Clone)")
        {
            Player.HoiMau();
        }

        if(gameObject.name == "AddMaxLives(Clone)")
        {
            Player.AddLives();
        }
    }

}
