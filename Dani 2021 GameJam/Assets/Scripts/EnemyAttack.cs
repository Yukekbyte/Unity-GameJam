using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public Animator anim;
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
    }
    void OnTriggerExit2D(Collider2D col)
    {  
        if (col.gameObject.CompareTag("Player"))
        {
        inAttackRange = false;
        }
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
            anim.SetTrigger("attack");
            cd = attackCooldown;
            Invoke("Damage", 1f);


        }
    }
    void Damage()
    {
        if (inAttackRange)
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(damage);
            
        }
    }
}
