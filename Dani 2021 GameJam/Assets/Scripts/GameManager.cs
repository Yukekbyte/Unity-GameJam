using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Wake()
    {
        DontDestroyOnLoad (gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenSettings()
    {
        Debug.Log("Open Settings");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
