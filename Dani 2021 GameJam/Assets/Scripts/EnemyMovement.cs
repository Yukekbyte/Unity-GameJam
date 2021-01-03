using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    public LayerMask GroundLayer;
    public float EnemySpeed;
    public float RoamRadius;
    public float VisionRadius;
    public bool StartRight;
    string Direction;
    float StartingPointX;
    bool EnemySpotted;

    public void Start() 
    {
        //sets direction to correct starting value
        if (StartRight)
        {
            Direction = "right";
        }
        else
        {
            Direction = "left";
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        //sets Starting position
        StartingPointX = transform.localPosition.x;
    }

    public void FixedUpdate()
    {   
        if (Falling())
        {
            return;
        }

        if (EnemySpotted)
        {
            Chase();
        }
        else
        {
            Roam();
        }
    }

    void Roam()
    {   
        if (Direction == "right")
        {
            if ((transform.localPosition.x) < ( StartingPointX + RoamRadius))
            {
                rb.velocity = new Vector2(EnemySpeed, 0);
            }
            else
            {
                Direction = "left";
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (Direction == "left")
        {
            if ((transform.localPosition.x) > ( StartingPointX - RoamRadius))
            {
                rb.velocity = new Vector2(-EnemySpeed, 0);
            }
            else
            {
                Direction = "right";
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void Chase()
    {
        Debug.Log("I'm chasing the player");
    }

    bool Falling() //Checks if enemy is falling
    {
        //Stuurt een ray van het midden van de speler naar beneden tot de rand van de boxcollider + margin
        //returns true wanneer de ray een object met groundlayer false, anders true
        float margin = 0.1f;
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, margin, GroundLayer);
        return ray.collider == null;
    }
}