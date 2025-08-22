using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Character
{
    public static Boss instance;
    protected Rigidbody2D rb;
    private Animator anim;
    private Player player;
    private Transform detectedPoint;
    private Transform target;
    private CapsuleCollider2D capsuleCollider;
    private GameObject bossRoom;
    protected float detectedRange = 15f;
    public LayerMask detectedLayer;

    private void Awake()
    {
        instance = this;
        detectedPoint = transform.Find("DetectedPoint");
        player = FindAnyObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider2D>();
        if (player != null)
        {
            target = player.transform;
        }
        if (characterHealthBar == null)
            characterHealthBar = GameObject.Find("BossHealthBar").GetComponent<Slider>();
        bossRoom = GameObject.Find("BossRoomPoint");
    }
    private void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Move(direction);
    }
    protected override void Move(Vector2 direction)
    {
        Collider2D hit = Physics2D.OverlapCircle(detectedPoint.position, detectedRange, detectedLayer);
        int random = Random.Range(1, 4);
        if (hit != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 15f && distance > 10f)
            {
                if (!BossAttack.instance.isAttackingSpelled && random == 2)
                {
                    BossAttack.instance.attackIndex = random;
                }
            }
            else if (distance <= 10f)
            {
                anim.SetFloat("isRunning", 0);

                if (!BossAttack.instance.isAttacking && !BossAttack.instance.isAttackingSpelled)
                {
                    BossAttack.instance.attackIndex = random; // 1: normal, 2: spell
                }
            }
            else
            {
                anim.SetFloat("isRunning", 1f);

                rb.linearVelocity = characterData.speed * (Vector3)direction;
                BossAttack.instance.attackIndex = 0;
            }
        }
        else
        {
            BossAttack.instance.attackIndex = 0;
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

        Vector3 strikePos = new(target.position.x, target.position.y + BossAttack.instance.spellOffsetY, 0);

        Instantiate(BossAttack.instance.spellPrefab, strikePos, Quaternion.identity);
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
        Destroy(bossRoom);
        Destroy(gameObject);
    }
}
