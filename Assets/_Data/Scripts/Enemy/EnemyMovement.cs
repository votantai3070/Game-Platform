using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public SpriteRenderer leftSprite;
    public SpriteRenderer rightSprite;
    private Transform point1;
    private Transform point2;

    private Vector3 targetPosition;
    private Animator anim;

    public float speed = 2f;

    public void SetPoints(Transform p1, Transform p2)
    {
        point1 = p1;
        point2 = p2;
        targetPosition = point1.position;
    }

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }


    private void FixedUpdate()
    {
        if (point1 == null || point2 == null) return;
        Movement();
    }



    private void Movement()
    {
        if (Vector3.Distance(transform.parent.position, targetPosition) < 0.1f)
        {
            // Switch target position
            targetPosition = targetPosition == point1.position ? point2.position : point1.position;
        }

        // Move towards the target position
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, speed * Time.deltaTime);
        FlipSprite();
    }

    private void FlipSprite()
    {
        bool isMovingLeft = targetPosition == point1.position;

        leftSprite.enabled = isMovingLeft;
        rightSprite.enabled = !isMovingLeft;

        anim.SetBool("isLeftRunning", isMovingLeft);
        anim.SetBool("isRightRunning", !isMovingLeft);
        anim.SetFloat("isRunLeft", isMovingLeft ? 1f : 0f);
        anim.SetFloat("isRunRight", isMovingLeft ? 0f : 1f);
    }
}
