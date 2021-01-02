using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.Play("AttackAnim(tijdelijk)");
    }
}
