using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Character
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;
    private Player player;

    [SerializeField] private Weapon bossWeapon;
    public GameObject spellPrefab;

    [SerializeField] private float spellOffsetY = 4f;
    private Transform target;
    private float detectedRange = 15f;
    private Transform detectedPoint;
    public LayerMask detectedLayer;
    private int attackIndex;
    private float attackDelay = 2f;
    //private float lastTimeAttack;
    private bool isAttacking = false;
    private bool isAttackingSpelled = false;


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
                anim.SetBool("isAttackingSpell", false);
                break;
            case 1:
                if (!isAttacking)
                    StartCoroutine(NormalAttack());
                break;
            case 2:
                if (!isAttackingSpelled) StartCoroutine(SpellAttack());
                break;

        }
    }

    protected override void Move(Vector2 direction)
    {
        Collider2D hit = Physics2D.OverlapCircle(detectedPoint.position, detectedRange, detectedLayer);

        int random = Random.Range(1, 3);
        if (hit != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 15f && distance > 10f)
            {
                if (!isAttackingSpelled && random == 2)
                {
                    attackIndex = random;
                }
            }
            else if (distance <= 10f)
            {
                anim.SetFloat("isRunning", 0);

                if (!isAttacking && !isAttackingSpelled)
                {
                    attackIndex = random; // 1: normal, 2: spell
                }
            }
            else
            {
                anim.SetFloat("isRunning", 1f);

                rb.linearVelocity = characterData.speed * (Vector3)direction;
                attackIndex = 0;
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

    public void CastSpell()
    {
        if (target == null) return;

        Vector3 strikePos = new(target.position.x, target.position.y + spellOffsetY, 0);

        Instantiate(spellPrefab, strikePos, Quaternion.identity);
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
                Debug.Log("damageable: " + damageable);
                Attack(damageable);
            }
        }
    }

    IEnumerator NormalAttack()
    {
        isAttacking = true;
        if (isAttacking)
        {
            anim.SetBool("isAttacking", true);
            anim.SetTrigger("isAttack");
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    IEnumerator SpellAttack()
    {
        isAttackingSpelled = true;
        if (isAttackingSpelled)
        {
            anim.SetBool("isAttackingSpell", true);
            anim.SetTrigger("isAttackSpell");
        }

        yield return new WaitForSeconds(attackDelay);
        isAttackingSpelled = false;

    }

    public void ActiveCollision()
    {
        capsuleCollider.enabled = true;
    }

    public void InactiveCollision()
    {
        capsuleCollider.enabled = false;
    }

    public void ResetAnimNormalAttack()
    {
        anim.SetBool("isAttacking", false);
    }
    public void ResetAnimSpellAttack()
    {
        anim.SetBool("isAttackingSpell", false);
    }


    protected override void Die()
    {
        StartCoroutine(WaitAnimDeadForSecond());
    }

    IEnumerator WaitAnimDeadForSecond()
    {
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }
}
