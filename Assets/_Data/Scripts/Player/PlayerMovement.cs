using System.Collections;
using UnityEngine;

public class PlayerMovement : Character
{
    [Header("Player Movement")]
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private Animator ani;

    private bool jumpPressed;
    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    [SerializeField] private float slowSpeed = 2f;
    [SerializeField] private float blockSpeed = 0.5f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        ani = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpPressed = Input.GetButtonDown("Jump");
        Jump();

    }

    void FixedUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        Move(direction);
    }


    protected override void Move(Vector2 direction)
    {
        bool isAttack = ani.GetCurrentAnimatorStateInfo(0).IsTag("isAttack");
        bool isBlock = ani.GetCurrentAnimatorStateInfo(0).IsTag("isBlock");

        if (direction.x != 0)
        {
            transform.parent.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }

        // If the player is attacking, slow down the movement speed
        if (isAttack)
        {
            rb.linearVelocity = new Vector2(direction.x * slowSpeed, rb.linearVelocity.y);
        }
        else if (isBlock)
        {
            rb.linearVelocity = new Vector2(direction.x * blockSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }

        ani.SetFloat("isRunning", Mathf.Abs(direction.x));

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
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

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
