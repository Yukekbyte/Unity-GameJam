using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float health;
    public Sprite enemyDeadSprite;

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
            Die();
        }
    }

    public void Die()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        gameObject.GetComponent<EnemyAttack>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<SpriteRenderer>().sprite = enemyDeadSprite;
            
        //Destory enemy Sword
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
