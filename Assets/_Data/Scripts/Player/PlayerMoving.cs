using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private Animator ani;
    private SpriteRenderer spriteRenderer;

    private float moveHorizontal;
    private bool jumpPressed;
    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    public float slowSpeed = 1f;


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
        bool isAttack = ani.GetCurrentAnimatorStateInfo(0).IsTag("isAttack");
        if (moveHorizontal != 0)
        {
            transform.parent.localScale = new Vector3(Mathf.Sign(moveHorizontal), 1, 1);
        }

        // If the player is attacking, slow down the movement speed
        if (isAttack)
        {
            rb.linearVelocity = new Vector2(moveHorizontal * slowSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        ani.SetFloat("isRunning", Mathf.Abs(moveHorizontal));
    }

    private void Jump()
    {
        if (isGrounded && jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (!isGrounded)
        {
            ani.SetBool("isJumping", true);
        }
        else
        {
            ani.SetBool("isJumping", false);
        }
    }
}
