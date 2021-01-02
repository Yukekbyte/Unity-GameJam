using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float health;

    void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            GameObject.FindObjectOfType<GameManager>().RestartLevel();
        }
    }
}
