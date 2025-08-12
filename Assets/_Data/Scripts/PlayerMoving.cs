using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveHorizontal;
    private bool jumpPressed;
    public float speed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    private Animator ani;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        ani = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetButtonDown("Jump");
        Jump();
    }

    private void FixedUpdate()
    {
        Movement();

    }
    private void Movement()
    {

        if (moveHorizontal != 0)
        {
            spriteRenderer.flipX = moveHorizontal < 0;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb.linearVelocity = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (isGrounded && jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
