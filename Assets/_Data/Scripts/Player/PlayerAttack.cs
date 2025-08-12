using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public CapsuleCollider2D attack1;

    private int comboIndex = 0;
    public float comboDelay = 0.5f;
    private float lastAttackTime;


    void Start()
    {
        anim = GetComponentInParent<Animator>();
        attack1.enabled = false;
    }

    void Update()
    {
        AnimationAttack();
    }

    private void AnimationAttack()
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
        attack1.enabled = true;
    }

    public void DisableAttackCollider()
    {
        attack1.enabled = false;
    }

}
