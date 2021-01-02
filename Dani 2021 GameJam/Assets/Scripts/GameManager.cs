using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public GameObject Main;
    public GameObject Settings;
    int MenuLayer;
    string MainMenuSceneName = "Menus";

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == MainMenuSceneName)
        {
            MenuLayer = 1;
            Main.SetActive(true);
            Settings.SetActive(false);
        }
        else
        {
            MenuLayer = 0;
            Main.SetActive(false);
            Settings.SetActive(false);
        }
    } 

    public void Update() // is called every frame
    {
        if (Input.GetKeyUp(KeyCode.Escape) && (MenuLayer == 0))
        {
            PauseGame();
            Main.SetActive(true);
            MenuLayer = 1;
        }
    }
    
    public void StartGame() // Restarts full game/Switches scene to level 1
    {
        SceneManager.LoadScene("Level1");
    }

    public void RestartLevel() // Restarts current level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel() //Loads next level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PauseGame() // Pauses Game
    {
        Time.timeScale = 0;
    }

    public void ResumeGame() // Resumes Game
    {
        Time.timeScale = 1;
    }

    public void OpenSettings() // Opens settings
    {
        Main.SetActive(false);
        Settings.SetActive(true);
        MenuLayer = 2;
    }
    public void GoBack() // Goes back one step in the UI
    {
        if (MenuLayer == 2)
        {
            Main.SetActive(true);
            Settings.SetActive(false);
            MenuLayer = 1;
        }
        if (MenuLayer == 1 && !(SceneManager.GetActiveScene().name == MainMenuSceneName))
        {
            ResumeGame();
            MenuLayer = 0;
        }

    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void QuitGame() // Exits Game
    {
        Application.Quit();
    }
}
