using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float playerSpeed;
    public float jumpForce;
    public BoxCollider2D boxCollider2D;
    public LayerMask GroundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump(jumpForce);
        }
    }
    void FixedUpdate()
    {
        //Player Input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        //Walk
        Walk(dir);
    }
    
    //Walk method met een direction (dir) nodig
    void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * playerSpeed * Time.deltaTime, rb.velocity.y);
    }
    //Dash method In Progress
    void Dash()
    {
        //In Progress
    }

    //Jump method met een jumpforce nodig
    void Jump(float jumpForce)
    {
        rb.velocity += new Vector2(0, jumpForce);
    }

    //Method die checkt ofdat je op de grond staat
    private bool IsGrounded()
    {
        //Stuurt een ray van het midden van de speler naar beneden tot de rand van de boxcollider + margin
        //returns true wanneer de ray een object met groundlayer raakt, anders false
        float margin = 0.1f;
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, margin, GroundLayer);
        return ray.collider != null;
    }
}
