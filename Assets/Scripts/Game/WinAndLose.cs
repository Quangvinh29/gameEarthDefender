using System.Collections;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SocialPlatforms.Impl;

public class WinAndLose : MonoBehaviour
{
    public GameObject FinishMenu;
    public TMP_Text ThongBao;
    public TMP_Text Mota;
    public TMP_Text HienThiDiem;

    public LocalizedString ThongBaoThang;
    public LocalizedString ThongBaoThua;

    public LocalizedString MotaThang;
    public LocalizedString MotaThua1;
    public LocalizedString MotaThua2;

    public LocalizedString ScoreString;

    private Score AddScore;
    private int FinalScore;

    private void Start()
    {
        AddScore = GameObject.Find("ScoreShow").GetComponent<Score>();
    }

    public IEnumerator WinGame()
    {
        yield return new WaitForSeconds(10f);
        FinishMenu.SetActive(true);

        ThongBaoThang.StringChanged += UpdateTextThongBao;
        MotaThang.StringChanged += UpdateTextMoTa;

        ThongBaoThang.RefreshString(); 
        MotaThang.RefreshString();

        HienThiDiemVaDung();
    }

    private void UpdateTextThongBao(string value)
    {
        ThongBao.text = value;
 
    }

    private void UpdateTextMoTa(string value)
    {
        Mota.text = value;
    }


    public IEnumerator PlayerDeathGameOver()
    {
        yield return new WaitForSeconds(3f);
        FinishMenu.SetActive(true);

        ThongBaoThua.StringChanged += UpdateTextThongBao;
        MotaThua1.StringChanged += UpdateTextMoTa;

        ThongBaoThua.RefreshString();
        MotaThua1.RefreshString();

        HienThiDiemVaDung();

    }

    public void EarthGameOver()
    {
        FinishMenu.SetActive(true);

        ThongBaoThua.StringChanged += UpdateTextThongBao;
        MotaThua2.StringChanged += UpdateTextMoTa;

        ThongBaoThua.RefreshString();
        MotaThua2.RefreshString();

        HienThiDiemVaDung();

    }

    private void HienThiDiemVaDung()
    {
        FinalScore = AddScore.FinalScore();

        ScoreString.Arguments = new object[] { FinalScore };
        ScoreString.StringChanged += UpdateTextScore;
        ScoreString.RefreshString();

        Time.timeScale = 0f;
    }

    private void UpdateTextScore(string value)
    {
        HienThiDiem.text = value;
    }
}
