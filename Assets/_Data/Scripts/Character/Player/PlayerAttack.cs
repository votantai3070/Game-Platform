using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Weapon))]
public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private Animator anim;
    public CapsuleCollider2D attackCollider;
    private Weapon weapon;

    [Header("Combo Settings")]
    private int comboIndex = 0;
    public float comboDelay = 0.5f;
    private float lastAttackTime;

    private void Awake()
    {
        player = GetComponent<Player>();
        weapon = GetComponent<Weapon>();
        anim = GetComponentInParent<Animator>();
        attackCollider.enabled = false;
        weapon.Init(player);
    }


    void Update()
    {
        HandleAttackInput();
    }


    private void Attack(IDamageable target)
    {
        if (weapon == null) return;
        weapon.UseWeapon(target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attackCollider.enabled) return;

        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Attack(damageable);
            }

        }
        else
        if (collision.CompareTag("Boss"))
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                Attack(damageable);
            }

        }
    }

    // Weapon setter method to change the weapon dynamically
    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }

    private void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastAttackTime > comboDelay)
            {
                comboIndex = 0; // Reset combo index if enough time has passed
            }

            comboIndex++;

            if (comboIndex > 3)
            {
                comboIndex = 1; // Reset to first attack if combo exceeds 3
            }

            // Set the attack animation based on the combo index
            anim.SetInteger("isAttack", comboIndex);
            anim.SetTrigger("isAttacking");

            lastAttackTime = Time.time;
        }
    }

    // This method should be called at the end of the attack animation
    public void EndAttack()
    {
        if (Time.time - lastAttackTime > comboDelay)
        {
            comboIndex = 0; // Reset combo index if enough time has passed
            anim.SetInteger("isAttack", 0); // Reset attack animation
        }
    }

    public void ResetCombo()
    {
        comboIndex = 0; // Reset combo index
        anim.SetInteger("isAttack", 0); // Reset attack animation
    }

    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }

    public void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }

}
