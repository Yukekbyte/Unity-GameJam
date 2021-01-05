using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Animation anim;
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage;
    public float attackRate;
    public LayerMask enemyLayers;
    public static int IsAttacking = 0;
    public float AttackDelay = 0f;
    private float nextAttackTime = 0f;

    public void Awake()
    {
        attackRange = 0.71f;
        attackDamage = 1;
        attackRate = 2;
    }

    public void Update()
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
        animator.SetTrigger("Attack");

        //Stop moving while attacking
        IsAttacking = 1;
        //gameObject.GetComponent<Rigidbody>().velocity = new Vector2(0,0);

        //Sphere gets called with attackpoint as origin and all enemies in the sphere are stored in an array
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Each enemy in the sphere takes damage with damage amount specified as public float
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }

        Invoke("Delay", AttackDelay);
    }

    //Draws attack sphere in scene for easy of use
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Delay() 
    {
        IsAttacking = 0;
    }
}
