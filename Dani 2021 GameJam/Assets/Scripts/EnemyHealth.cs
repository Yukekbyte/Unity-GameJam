using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float health;

    void Awake()
    {
        health = maxHealth;
    }

    //TakeDamage method with amount needed    vb: TakeDamage(10);
    public void TakeDamage(float amount)
    {
        health -= amount;

        Debug.Log("I took "+amount + " damage!");
        
        //Destroys object if health gone
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
