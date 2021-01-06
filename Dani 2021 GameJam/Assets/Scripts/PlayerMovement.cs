using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float playerSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float dashRate;
    public bool dashEnabled;
    public float jumpForce;
    public float slideSpeed;
    public float wallJumpForce;
    public float wallJumpDuration;
    public bool wallJumpEnabled;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    public LayerMask groundLayer;
    public SpriteRenderer spriteRenderer;
    private float collisionRadius = 0.1f;
    private float xScale;
    private bool onWall;
    private bool onWallLeft;
    private bool onWallRight;
    private bool wallJumping;
    private float wallJumpTimer;
    private float nextDashAvailable;
    private float dashTimer;
    private bool dashing;
    private Vector2 dashDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        xScale = transform.localScale.x;
        wallJumpEnabled = false;
        dashEnabled = false;
    }

    void Update()
    {   
        //Check if onWall with circles
        onWallRight = Physics2D.OverlapCircle((Vector2)transform.position + boxCollider2D.size.x/2 * Vector2.right - 0.4f * boxCollider2D.size.y * Vector2.up, collisionRadius, groundLayer)
                   && Physics2D.OverlapCircle((Vector2)transform.position + boxCollider2D.size.x/2 * Vector2.right + 0.1f * boxCollider2D.size.y * Vector2.up, collisionRadius, groundLayer);
        onWallLeft = Physics2D.OverlapCircle((Vector2)transform.position - boxCollider2D.size.x/2 * Vector2.right - 0.4f * boxCollider2D.size.y * Vector2.up, collisionRadius, groundLayer) 
                  && Physics2D.OverlapCircle((Vector2)transform.position - boxCollider2D.size.x/2 * Vector2.right + 0.1f * boxCollider2D.size.y * Vector2.up, collisionRadius, groundLayer);
        onWall = onWallLeft || onWallRight;

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {   

            //als de speler alleen op de grond staat jumpt hij normaal
            if (IsGrounded())
            {   
                animator.SetBool("IsJumping", true);
                Jump();
            }
            //bij een wallslide springt de speler ook weg van de muur
            else if (onWall && wallJumpEnabled && !wallJumping)
            {   
                animator.SetBool("IsJumping", true);
                wallJumping = true;
                wallJumpTimer = wallJumpDuration;
                if (onWallRight)
                {
                    WallJump(-1);
                }
                else if (onWallLeft)
                {
                    WallJump(1);
                }
                
            }
        }

        if (wallJumping)
        {
            wallJumpTimer -= Time.deltaTime;
            if (wallJumpTimer < 0)
            {
                wallJumping = false;
            }
        }
        if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }

        //dash
        bool playerMoveInput = (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);

        if (Input.GetKeyDown(KeyCode.LeftShift) && (Time.time >= nextDashAvailable) && dashEnabled && playerMoveInput)
        {   
            float dashDirectionX = Input.GetAxis("Horizontal");
            float dashDirectionY = Input.GetAxis("Vertical");
            dashDirection = new Vector2(dashDirectionX, dashDirectionY);

            dashing = true;
            nextDashAvailable = Time.time + 1f/dashRate;
            dashTimer = dashDuration;

            Dash();
        }
        if (dashing)
        {   
            dashTimer -= Time.deltaTime;
            if (dashTimer < 0)
            {
                dashing = false;
                rb.velocity = new Vector2(0, 0);
            }
        }

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
        float dir = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(dir));

    if (!dashing && !wallJumping && PlayerAttack.IsAttacking == 0)
        {
            //Walk
            Walk(dir);

            //Player faces his direction
            if(dir > 0)
            {
                transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
            }
            if(dir < 0)
            {
                transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
            }
        }

        
    }

    //Walk method met een direction (dir) nodig
    void Walk(float dir)
    {
        rb.velocity = new Vector2(dir * playerSpeed * Time.deltaTime, rb.velocity.y);
    }
    
    //Dash method
    void Dash()
    {
        
        rb.velocity += new Vector2(dashDirection.x * dashSpeed, dashDirection.y * dashSpeed);
    }

    //Wallslide
    void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed * Time.deltaTime);
    }

    void WallJump(int directionMultiplier)
    {
        rb.velocity = new Vector2(directionMultiplier * wallJumpForce, jumpForce);
        transform.localScale = new Vector3(xScale * directionMultiplier, transform.localScale.y, transform.localScale.z);
    }

    //Jump method met een jumpforce nodig
    void Jump()
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
