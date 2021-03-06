using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public GameObject Main;
    public GameObject Settings;
    public PlayerAbilities playerAbilities;
    public PlayerAttack playerAttack;
    bool SceneIsLevel;
    int MenuLayer;

    public void Start()
    {   
        playerAbilities = GameObject.FindObjectOfType<PlayerAbilities>();
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        playerAttack.Delay();
        
        switch (SceneManager.GetActiveScene().name)
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
        SceneManager.LoadScene("CutsceneStart");
        Time.timeScale = 1;
        GameObject.FindObjectOfType<AudioManager>().Play("Start Game Button");
        GameObject.FindObjectOfType<AudioManager>().Stop("Theme");
    }

    public void RestartLevel() // Restarts current level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
        GameObject.FindObjectOfType<AudioManager>().Play("Button");
    }

    public void LoadNextLevel() //Loads next level
    {
        PlayerAbilities.soulsBeforeActiveLevel = playerAbilities.souls;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject.FindObjectOfType<AudioManager>().Play("Button");
    }

    public void PauseGame() // Pauses Game
    {
        Time.timeScale = 0;
    }

    public void ResumeGame() // Resumes Game
    {
        Time.timeScale = 1;
        GameObject.FindObjectOfType<AudioManager>().Play("Button");
    }

    public void OpenSettings() // Opens settings
    {
        Main.SetActive(false);
        Settings.SetActive(true);
        MenuLayer = 2;
        GameObject.FindObjectOfType<AudioManager>().Play("Button");
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
        GameObject.FindObjectOfType<AudioManager>().Play("Button");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameObject.FindObjectOfType<AudioManager>().Play("Button");
    }

    public void QuitGame() // Exits Game
    {
        Application.Quit();
    }
}
