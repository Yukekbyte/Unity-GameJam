using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animation anim;
    private bool inAttackRange = false;
    public float damage;
    public float attackCooldown;
    private float cd;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inAttackRange = true;
        }
        if (col.gameObject.tag != "Player")
        {
            inAttackRange = false;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        inAttackRange = false;
    }


    void Update()
    {
        if (inAttackRange)
        {
            Attack();
        }

        //cooldowntimer tikt af met 1 per seconde
        cd -= Time.deltaTime;
    }

    void Attack()
    {
        if (cd < 0)
        {
            anim.Play("EnemyAttackAnim");
            FindObjectOfType<PlayerHealth>().TakeDamage(damage);
            cd = attackCooldown;
        }
    }
}
