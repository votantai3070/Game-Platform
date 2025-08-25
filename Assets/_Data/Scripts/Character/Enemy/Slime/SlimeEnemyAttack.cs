using UnityEngine;

public class SlimeEnemyAttack : MonoBehaviour
{
    [Header("Slime Enemy Attack Settings")]
    private Slime slime;

    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;
    private bool isCrit = false;

    private void Awake()
    {
        slime = GetComponentInParent<Slime>();
    }



    private void Attack(IDamageable target)
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }
        target.TakeDamage(slime.Damage, isCrit);
        lastAttackTime = Time.time;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Attack(damageable);
            }
        }
        if (collision.CompareTag("PlayerBlock"))
        {
            var playerBlock = collision.GetComponentInParent<PlayerBlock>();
            if (playerBlock != null)
            {
                Debug.Log("playerBlock (parent): " + playerBlock);
                playerBlock.BlockedEnemy();
            }
        }
    }
}
