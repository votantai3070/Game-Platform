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
    [SerializeField] private int dashForce = 100;
    [SerializeField] private float dashTime = 0.5f;
    //[SerializeField] private float nextDashTime = 0f;
    //[SerializeField] private float dashCoolDown = 0.5f;
    private bool isDashing = false;
    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        ani = GetComponentInParent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        DirectionPlayer();
        jumpPressed = Input.GetButtonDown("Jump");
        Jump();
        Dash(direction);
    }

    void FixedUpdate()
    {
        Move(direction);
    }

    void DirectionPlayer()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
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
        else if (!isDashing)
        {
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }

        ani.SetFloat("isRunning", Mathf.Abs(direction.x));

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Dash(Vector2 direction)
    {
        Debug.Log("Dashing: " + isDashing);
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && isGrounded)
        {
            StartCoroutine(PLayerDashing(direction));
        }
    }

    IEnumerator PLayerDashing(Vector2 direction)
    {
        isDashing = true;
        //nextDashTime = Time.fixedDeltaTime + dashCoolDown;

        ani.SetBool("isDashing", true);

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);

        float originGravity = rb.gravityScale;

        rb.gravityScale = 0;

        Player.instance.InactiveCircleCollision();

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originGravity;
        ani.SetBool("isDashing", false);
        isDashing = false;
        Player.instance.ActiveCircleCollision();
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
