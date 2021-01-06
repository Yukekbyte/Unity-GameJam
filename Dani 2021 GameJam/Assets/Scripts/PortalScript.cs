using UnityEngine;

public class PortalScript : MonoBehaviour
{
    bool enemiesYes;
    void OnTriggerEnter2D()
    {
        
        //only pass through if all enemies have been eaten
        if(!enemiesYes)
        {
            GameObject.FindObjectOfType<AudioManager>().Play("Complete Level");
            GameObject.FindObjectOfType<GameManager>().LoadNextLevel();
        } else
        {
            GameObject.FindObjectOfType<PlayerAbilities>().DisplayAbilityInfo("Get Revenge!", "Make sure to devour all the enemies before advancing to the next level.");
        }
    }
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesYes = enemies.Length > 0;
    }
}
