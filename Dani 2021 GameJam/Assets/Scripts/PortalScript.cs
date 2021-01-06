using UnityEngine;

public class PortalScript : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("Complete Level");
        GameObject.FindObjectOfType<GameManager>().LoadNextLevel();
    }
}
