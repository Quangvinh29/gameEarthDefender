
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameControl : MonoBehaviour
{
    public GameObject BackGround;

    private void Start()
    {
        BackGround.GetComponent<AudioSource>().enabled = false;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("MainGame");
        BackGround.GetComponent<AudioSource>().enabled = true;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
