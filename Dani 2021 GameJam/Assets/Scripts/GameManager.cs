using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public void Wake()
    {
        DontDestroyOnLoad (gameObject);
    }

    public void StartGame() // Restarts full game/Switches scene to level 1
    {
        SceneManager.LoadScene("Level1");
    }

    public void RestartLevel() // Restarts current level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenSettings() // Opens settings
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    public void GoBack() // Goes back one step in the UI
    {
        if (SceneManager.GetActiveScene().name == "Menus")
        {
            MainMenu.SetActive(true);
            SettingsMenu.SetActive(false);
        }
        else
        {
            Debug.Log("placeholder");
        }

    }

    public void QuitGame() // Exits Game
    {
        Application.Quit();
    }
}
