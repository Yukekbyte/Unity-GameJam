using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform playerTransform;
    private bool inAttackRange;
    public float attackRange;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
        foreach(Collider2D hit in hits)
        {
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
