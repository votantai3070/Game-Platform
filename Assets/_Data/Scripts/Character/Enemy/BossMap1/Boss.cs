using System.Collections;
using UnityEngine;
public class Boss : Character
{
    public Rigidbody2D rb;
    public Animator anim;
    public Player player;
    public Transform detectedPoint;
    private Transform target;
    public CapsuleCollider2D capsuleCollider;
    public GameObject bossRoom;
    private float detectedRange = 15f;
    public LayerMask detectedLayer;

    protected override void Start()
    {
        base.Start();
        if (player != null)
        {
            target = player.transform;
        }
    }
    private void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Move(direction);
    }
    protected override void Move(Vector2 direction)
    {
        Collider2D hit = Physics2D.OverlapCircle(detectedPoint.position, detectedRange, detectedLayer);
        Debug.Log($"hit: {hit}");

        if (hit == null)
        {
            BossAttack.instance.attackIndex = 0;
            anim.SetFloat("isRunning", 0);
            Flip(direction);
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        anim.SetFloat("isRunning", 0);

        if (distance > 10f && distance < detectedRange)
        {
            anim.SetFloat("isRunning", 1f);
            rb.linearVelocity = characterData.speed * (Vector3)direction;
            BossAttack.instance.attackIndex = 0;
        }
        else
        {
            int random = Random.Range(1, 4);

            if (distance <= 12f && distance > 8f && random == 2 && !BossAttack.instance.isAttackingSpelled)
            {
                BossAttack.instance.attackIndex = 2;
            }
            else if (distance <= 8f && !BossAttack.instance.isAttacking && !BossAttack.instance.isMinionSpawned)
            {
                if (random == 1 || random == 3)
                    BossAttack.instance.attackIndex = random;
            }
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
