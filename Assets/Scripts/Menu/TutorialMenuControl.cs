
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenuControl : MonoBehaviour
{
    public GameObject[] tutorials;
    public GameObject pauseMenu;

    private int CurrentTutorial = 0;
    private bool DaXemTutorial = false;

   void Start()
    {
        DaXemTutorial = false;
        HienTutorial(CurrentTutorial);
         Time.timeScale = 0f;
    }

    private void HienTutorial(int i)
    {
        if (i < tutorials.Length)
        {
            tutorials[i].SetActive(true);
        }
    }

    public void NextTutorial()
    {
       tutorials[CurrentTutorial].SetActive(false);
       CurrentTutorial++;
        if (CurrentTutorial < tutorials.Length)
        {
           HienTutorial(CurrentTutorial);
        }
        else
        {
            FinishTutorial();
        }
    }

    public void SkipTutorial()
    {
        FinishTutorial();
    }

    private void FinishTutorial()
    {
        DaXemTutorial = true;
        foreach (var tutorial in tutorials)
        {
            tutorial.SetActive(false);
        }

        CurrentTutorial = 0;
        Time.timeScale = 1f;
    }

    void OnApplicationPause(bool pauseStatus)
    {

        if (pauseStatus == true)
        {
            if (DaXemTutorial == false)
            {
                return;
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

    }

}
