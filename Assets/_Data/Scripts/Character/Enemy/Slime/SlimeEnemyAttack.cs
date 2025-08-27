using System.Collections;
using UnityEngine;

public class SlimeEnemyAttack : MonoBehaviour
{
    [Header("Slime Enemy Attack Settings")]
    private Slime slime;

    [SerializeField] private float attackCooldown = 1f;
    private bool isCrit = false;
    private bool canAttack = true;

    private void Awake()
    {
        slime = GetComponentInParent<Slime>();
    }

    private void Attack(IDamageable target)
    {
        target.TakeDamage(slime.Damage, isCrit);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canAttack)
                StartCoroutine(AttackRoutine(collision));
        }
        if (collision.CompareTag("PlayerBlock"))
        {
            var playerBlock = collision.GetComponentInParent<PlayerBlock>();
            if (playerBlock != null)
            {
                playerBlock.BlockedEnemy();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            canAttack = true;
        }
    }

    IEnumerator AttackRoutine(Collider2D player)
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        canAttack = false;
        while (damageable != null)
        {
            Attack(damageable);
            yield return new WaitForSeconds(attackCooldown);
        }
    }
}
