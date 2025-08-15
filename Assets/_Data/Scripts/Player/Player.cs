using UnityEngine;

public class Player : Character
{
    private Animator _animator;
    private bool _isPlaying;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Die()
    {

        _animator.SetTrigger("isDead");

        PlayerMovement playerMovement = GetComponentInChildren<PlayerMovement>();
        Debug.LogWarning("playerMovement: " + playerMovement);
        playerMovement.enabled = false;

        if (TryGetComponent<PlayerAttack>(out var playerAttack))
            Debug.LogWarning("playerAttack: " + playerAttack);
        playerAttack.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

    }

}
