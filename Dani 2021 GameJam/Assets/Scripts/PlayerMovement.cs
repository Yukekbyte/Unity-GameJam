using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float jumpForce;
    public float slideSpeed;
    public float wallJumpForce;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    public LayerMask groundLayer;
    public SpriteRenderer spriteRenderer;
    private float collisionRadius = 0.1f;
    private float xScale;
    private bool onWall;
    private bool onWallLeft;
    private bool onWallRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        xScale = transform.localScale.x;
    }

    void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //als de speler alleen op de grond staat jumpt hij normaal
            if (IsGrounded())
            {
                Jump(jumpForce);
            }
            //bij een wallslide springt de speler ook weg van de muur
            else if (onWallRight)
            {
                rb.velocity = new Vector2(wallJumpForce, jumpForce);
            }
            else if (onWallLeft)
            {
                rb.velocity = new Vector2(-wallJumpForce, jumpForce);
            }
        }

        //Check if onWall with circles
        onWallRight = Physics2D.OverlapCircle((Vector2)transform.position + boxCollider2D.size.x/2 * Vector2.right - 0.4f * boxCollider2D.size.y * Vector2.up, collisionRadius, groundLayer) && Physics2D.OverlapCircle((Vector2)transform.position + boxCollider2D.size.x/2 * Vector2.right + 0.1f * boxCollider2D.size.y * Vector2.up, collisionRadius, groundLayer);
        onWallLeft = Physics2D.OverlapCircle((Vector2)transform.position - boxCollider2D.size.x/2 * Vector2.right - 0.4f * boxCollider2D.size.y * Vector2.up, collisionRadius, groundLayer) && Physics2D.OverlapCircle((Vector2)transform.position - boxCollider2D.size.x/2 * Vector2.right + 0.1f * boxCollider2D.size.y * Vector2.up, collisionRadius, groundLayer);
        onWall = onWallLeft || onWallRight;

        //Wallside wanneer de speler op de muur is en niet de grond
        if (onWall && !IsGrounded() && rb.velocity.y < 0)
        {
            WallSlide();
        }
    }
    //Gizmos om de cirkels die onWall bepalen te tekenen op je scherm
    /*
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + boxCollider2D.size.x/2 * Vector2.right + 0.1f * boxCollider2D.size.y * Vector2.up, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + boxCollider2D.size.x/2 * Vector2.right - 0.4f * boxCollider2D.size.y * Vector2.up, collisionRadius);
    }
    */
    
    void FixedUpdate()
    {
        //Player Input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        //Walk
        Walk(dir);

        //Player faces his direction
        if(dir.x > 0)
        {
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }
        if(dir.x < 0)
        {
            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
        }
    }
    
    //Walk method met een direction (dir) nodig
    void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * playerSpeed * Time.deltaTime, rb.velocity.y);
    }
    
    //Dash method
    void Dash()
    {
        //In Progress
    }

    //Wallslide
    void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed * Time.deltaTime);
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
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, margin, groundLayer);
        return ray.collider != null;
    }
}
