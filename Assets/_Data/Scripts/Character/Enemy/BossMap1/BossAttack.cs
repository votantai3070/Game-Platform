using System.Collections;
using UnityEngine;

public class BossAttack : Character
{
    [Header("Boss Attack Settings")]
    public static BossAttack instance;
    private CapsuleCollider2D capsuleCollider;
    [SerializeField] private Weapon bossWeapon;
    public GameObject spellPrefab;
    [HideInInspector] public float attackDelay = 2f;
    [HideInInspector] public float spellOffsetY = 4f;
    private Animator ani;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isAttackingSpelled = false;
    [HideInInspector] public int attackIndex;
    [HideInInspector] public bool isMinionSpawned = false;
    public Transform createMinionPoint;
    private BossSpawnSlimePool bossSpawnSlimePool;
    private Boss boss;

    private void Awake()
    {
        instance = this;
        ani = GetComponentInParent<Animator>();
        boss = GetComponentInParent<Boss>();
        bossWeapon.Init(boss);
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        bossSpawnSlimePool = FindAnyObjectByType<BossSpawnSlimePool>();
    }

    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        switch (attackIndex)
        {
            case 0:
                ani.SetBool("isAttacking", false);
                ani.SetBool("isAttackingSpell", false);
                break;
            case 1:
                if (!isAttacking)
                    StartCoroutine(NormalAttack());
                break;
            case 2:
                if (!isAttackingSpelled)
                    StartCoroutine(SpellAttack());
                break;
            case 3:
                if (!isMinionSpawned)
                    StartCoroutine(CreateMinions());
                break;
        }
    }

    public IEnumerator NormalAttack()
    {
        isAttacking = true;
        if (isAttacking)
        {
            ani.SetBool("isAttacking", true);
            ani.SetTrigger("isAttack");
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    public IEnumerator SpellAttack()
    {
        isAttackingSpelled = true;
        if (isAttackingSpelled)
        {
            ani.SetBool("isAttackingSpell", true);
            ani.SetTrigger("isAttackSpell");
        }

        yield return new WaitForSeconds(attackDelay);
        isAttackingSpelled = false;

    }

    IEnumerator CreateMinions()
    {
        isMinionSpawned = true;
        if (isMinionSpawned)
        {
            ani.SetBool("isAttackingSpell", true);
            ani.SetTrigger("isAttackSpell");
            bossSpawnSlimePool.GetSpawnMinions();
        }
        yield return new WaitForSeconds(attackDelay);

        isMinionSpawned = false;
    }

    protected override void Attack(IDamageable t)
    {
        Debug.Log("Boss Attack");
        bossWeapon.UseWeapon(t);
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
        if (collision.CompareTag("PlayerBlock"))
        {
            PlayerBlock playerBlock = collision.GetComponentInParent<PlayerBlock>();
            if (playerBlock == null) return;
            playerBlock.BlockedEnemy();
            Debug.Log("Player Blocked Enemy");

        }
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
