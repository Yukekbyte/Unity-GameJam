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
        //Enemy doesn't move
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //Removing unneeded scripts
        Destroy(GetComponent<EnemyAttack>());
        Destroy(GetComponent<EnemyMovement>());
        
        //Boxcollider and sprite change
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = enemyDeadSprite;

        //Add new script for when the enemy is dead
        gameObject.AddComponent<EnemyConsume>();

        //Destory enemy Children
        foreach (Transform child in transform)
        {
            if(child.name != "Canvas")
                Destroy(child.gameObject);
        }
    }
}
