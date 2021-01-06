using UnityEngine;

public class PortalScript : MonoBehaviour
{
    bool yes;
    void OnTriggerEnter2D()
    {
        yes = GameObject.FindGameObjectsWithTag("Enemy") == null;
        //only pass through if all enemies have been eaten
        if(yes)
        {
            GameObject.FindObjectOfType<AudioManager>().Play("Complete Level");
            GameObject.FindObjectOfType<GameManager>().LoadNextLevel();
        } else
        {
            GameObject.FindObjectOfType<PlayerAbilities>().DisplayAbilityInfo("Get Revenge!", "Make sure to devour all the enemies before advancing to the next level.");
        }

        print(yes);
    }
    void Update()
    {
        print(yes);
    }
}
