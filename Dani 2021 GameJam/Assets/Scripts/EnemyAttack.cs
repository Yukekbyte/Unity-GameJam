using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool inAttackRange;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            inAttackRange = true;
        }
        if(col.gameObject.tag != "Player")
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
            //attackCode
        }
        Debug.Log(inAttackRange);
    }
}
