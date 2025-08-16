using System.Collections;
using UnityEngine;

public class Boss : Character
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;
    private Player player;

    [SerializeField] private Weapon bossWeapon;
    private Transform target;
    private float detectedRange = 10f;
    private Transform detectedPoint;
    public LayerMask detectedLayer;
    private int attackIndex;
    private float attackDelay = 2f;
    //private float lastTimeAttack;
    private bool isAttacking = false;


    private void Awake()
    {
        detectedPoint = transform.Find("DetectedPoint");
        player = FindAnyObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        bossWeapon.Init(this);
        if (player != null)
        {
            Debug.Log("Player: " + player);
            target = player.transform;
        }
    }



    private void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Move(direction);
        HandleAttack();
    }

    private void HandleAttack()
    {
        switch (attackIndex)
        {
            case 0:
                anim.SetBool("isAttacking", false);
                break;
            case 1:
                if (!isAttacking)
                    StartCoroutine(NormalAttack());
                break;
                //case 2:

        }
    }

    protected override void Move(Vector2 direction)
    {
        Collider2D hit = Physics2D.OverlapCircle(detectedPoint.position, detectedRange, detectedLayer);

        if (hit != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance < 4f)
            {
                anim.SetFloat("isRunning", 0);
                attackIndex = 1;
            }
            else
            {
                anim.SetFloat("isRunning", 1f);
                rb.linearVelocity = characterData.speed * (Vector3)direction;
            }
        }
        else
        {
            attackIndex = 0;
            anim.SetFloat("isRunning", 0);
        }

        Flip(direction);
    }

    private void Flip(Vector2 direction)
    {
        if (direction.x > 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), 1, 1);
        else if (direction.x < 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 1, 1);
    }

    protected override void Attack(IDamageable target)
    {
        bossWeapon.UseWeapon(target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (capsuleCollider == null) return;

        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                Attack(damageable);
            }
        }
    }

    IEnumerator NormalAttack()
    {
        isAttacking = true;

        anim.SetBool("isAttacking", true);
        anim.SetTrigger("isAttack");

        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    public void ActiveCollision()
    {
        capsuleCollider.enabled = true;
    }

    public void InactiveCollision()
    {
        capsuleCollider.enabled = false;
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
