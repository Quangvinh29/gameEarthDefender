using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public Image OptionMenu;
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1f;
    }

    public void OpenOptions()
    {
 
        OptionMenu.gameObject.SetActive(true);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
