using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public static Player instance;
    private Animator _animator;
    private CircleCollider2D _circleCollider;

    private void Awake()
    {
        instance = this;
        _animator = GetComponent<Animator>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }
    public void ActiveCircleCollision()
    {
        _circleCollider.enabled = true;
    }

    public void InactiveCircleCollision()
    {
        _circleCollider.enabled = false;
    }

    protected override void Die()
    {

        _animator.SetTrigger("isDead");

        PlayerMovement playerMovement = GetComponentInChildren<PlayerMovement>();
        playerMovement.enabled = false;

        if (TryGetComponent<PlayerAttack>(out var playerAttack))
            playerAttack.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

    }
}
