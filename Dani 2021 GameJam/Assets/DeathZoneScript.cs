using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            GameObject.FindObjectOfType<GameManager>().RestartLevel();       
    }
}
