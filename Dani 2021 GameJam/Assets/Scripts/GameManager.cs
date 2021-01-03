using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public GameObject Main;
    public GameObject Settings;
    bool SceneIsLevel;
    int MenuLayer;
    
    public void Start()
    {   switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
            {
                SceneIsLevel = false;
                MenuLayer = 1;
                Main = GameObject.Find("Main");
                Settings = GameObject.Find("Settings");
                Main.SetActive(true);
                Settings.SetActive(false);
                break;
            }
            case "EndScreen":
            {
                SceneIsLevel = false;
                MenuLayer = 1;
                Main = GameObject.Find("Main");
                Main.SetActive(true);
                break;
            }
            default:
            {
                SceneIsLevel = true;
                MenuLayer = 0;
                Main.SetActive(false);
                Settings.SetActive(false);
                break;
            }
        }
    } 

    public void Update() // is called every frame
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (MenuLayer == 0)
            {
                PauseGame();
                Main.SetActive(true);
                MenuLayer = 1;
            }
            else
            {
                GoBack();
            }
        }
        
    }
    
    public void StartGame() // Restarts full game/Switches scene to level 1
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }

    public void RestartLevel() // Restarts current level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
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
        if (MenuLayer == 1 && (SceneIsLevel))
        {
            ResumeGame();
            Main.SetActive(false);
            MenuLayer = 0;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() // Exits Game
    {
        Application.Quit();
    }
}
