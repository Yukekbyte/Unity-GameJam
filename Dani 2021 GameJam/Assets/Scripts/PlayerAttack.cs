using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animation anim;
    public Transform attackPoint;
    public float attackRange;
    public float damage;
    public LayerMask enemyLayers;
    

    void Update()
    {
        //Attack when left click
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        //Attack animation of the player
        anim.Play("AttackAnim(tijdelijk)");

        //Sphere gets called with attackpoint as origin and all enemies in the sphere are stored in an array
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Each enemy in the sphere takes damage with damage amount specified as public float
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
