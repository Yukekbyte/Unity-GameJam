using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    public LayerMask GroundLayer;
    public GameObject Player;
    public Animator Animator;
    public float EnemySpeed;
    public float RoamRadius;
    public float VisionRadius;
    public float ChaseBuffer;
    public bool StartRight;
    string Direction = "right";
    float StartingPointX;
    bool FreeRoamEnabled = false;
    Vector3 PreviousFrameLocation;

    public void Start() 
    {
        Player = GameObject.Find("Player");
        //sets direction to correct starting value
        if (!StartRight)
        {
            ChangeDirectionTo("left");
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

        if (PlayerSpotted())
        {
            Chase();
        }
        else
        {
            if (FreeRoamEnabled)
            {
                FreeRoam();
            }
            else
            {
                Roam();
            }
            
        }
    }

    void Roam()
    {
        if (transform.localPosition == PreviousFrameLocation)
        {
            FreeRoamEnabled = true;
        }

        if ((Direction == "right") && (transform.localPosition.x > ( StartingPointX + RoamRadius)))
        {
            ChangeDirectionTo("left");
        }
        else if ((Direction == "left") && (transform.localPosition.x < ( StartingPointX - RoamRadius)))
        {   
            ChangeDirectionTo("right");
        }

        PreviousFrameLocation = transform.localPosition;
        rb.velocity = new Vector2(EnemySpeed, 0);
    }

    void FreeRoam()
    {
        if ((Direction == "right") && (transform.localPosition == PreviousFrameLocation))
        {
            ChangeDirectionTo("left");
        }
        else if ((Direction == "left") && (transform.localPosition == PreviousFrameLocation))
        {
            ChangeDirectionTo("right");
        }
        PreviousFrameLocation = transform.localPosition;
        rb.velocity = new Vector2(EnemySpeed, 0);
    }

    void Chase()
    {
        if ((transform.localPosition.x < Player.transform.position.x) && (Direction != "right"))
        {
            ChangeDirectionTo("right");
        }
        else if ((transform.localPosition.x > Player.transform.position.x) && (Direction != "left"))
        {
            ChangeDirectionTo("left");
        }

        if ( ( (Player.transform.position.x + ChaseBuffer) < transform.localPosition.x) && (transform.localPosition.x < (Player.transform.position.x + ChaseBuffer) ) )
        {
            return;
        }
        else
        {
            rb.velocity = new Vector2(EnemySpeed, 0);
        }
    }

    bool Falling() //Checks if enemy is falling
    {
        //Stuurt een ray van het midden van de speler naar beneden tot de rand van de boxcollider + margin
        //returns true wanneer de ray een object met groundlayer false, anders true
        float margin = 0.1f;
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, margin, GroundLayer);
        return ray.collider == null;
    }

    void ChangeDirectionTo(string DirectionToChange)
    {
        Direction = DirectionToChange;
        EnemySpeed = EnemySpeed * -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    bool PlayerSpotted()
    {
        float Distance = Vector3.Distance (transform.localPosition, Player.transform.position);
        if (Distance < VisionRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}