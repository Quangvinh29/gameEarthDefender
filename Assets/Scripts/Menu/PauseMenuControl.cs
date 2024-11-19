
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{
    public Image PauseMenu;
    public Image OptionMenu;

    public GameObject BackGround;


    public void OpenPauseMenu()
    {
        PauseMenu.gameObject.SetActive(true);
        BackGround.GetComponent<AudioSource>().enabled = false;
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        BackGround.GetComponent<AudioSource>().enabled = true;
    }

    public void OpenOption()
    {
       OptionMenu.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
