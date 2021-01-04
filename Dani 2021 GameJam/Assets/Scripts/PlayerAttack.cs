using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animation anim;
    public Transform attackPoint;
    public float attackRange;
    public float attackDamage;
    public float attackRate;
    public LayerMask enemyLayers;

    
    private float nextAttackTime = 0f;

    void Update()
    {
        //Attack when left click
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f/attackRate;
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
            if (enemy.CompareTag("Enemy"))
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    //Draws attack sphere in scene for easy of use
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void AttackSpeedUp()
    {
        attackRate *= 2;
    }

    public void AttackDamageUp()
    {
        attackDamage++;
    }
    public void AttackRangeUp()
    {
        // attackRange vergroten
    }
}
