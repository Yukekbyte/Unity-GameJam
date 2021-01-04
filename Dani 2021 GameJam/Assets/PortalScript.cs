using UnityEngine;

public class PortalScript : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        GameObject.FindObjectOfType<GameManager>().LoadNextLevel();
    }
}
