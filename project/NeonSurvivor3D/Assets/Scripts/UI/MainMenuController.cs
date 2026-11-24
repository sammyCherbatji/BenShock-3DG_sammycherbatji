
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadLevel1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void LoadLevel2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button pressed!");
        Application.Quit();
    }
}