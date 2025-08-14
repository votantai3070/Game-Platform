using UnityEngine;

public class SlimeEnemyAttack : MonoBehaviour
{
    [Header("Slime Enemy Attack Settings")]
    private Slime slime;

    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

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
        Debug.Log($"{slime.characterData.characterName} is attacking {target?.GetType().Name}");
        target.TakeDamage(slime.Damage);
        lastAttackTime = Time.time;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Debug.Log($"{slime.characterData.characterName} detected {damageable} in range.");
                Attack(damageable);
            }
        }
    }


}
