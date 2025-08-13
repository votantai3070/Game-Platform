using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    private Animator anim;

    private float blockDuration = 0.1f;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        boxCollider2D.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            HandleBlock();
        }
        else
        {
            StopBlock();
        }
    }

    private void HandleBlock()
    {
        anim.SetBool("isBlocking", true);
        boxCollider2D.enabled = true;
    }


    private void StopBlock()
    {
        boxCollider2D.enabled = false;
        anim.SetBool("isBlocking", false);
    }

    public void BlockedEnemy()
    {
        anim.SetBool("isBlocking", false);
        anim.SetTrigger("isBlockedEnemy");
    }
}
