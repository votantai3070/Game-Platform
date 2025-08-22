using UnityEngine;

public class SlimeEnemyMovement : MonoBehaviour
{
    [Header("Slime Enemy Movement Settings")]
    private Slime slime;
    public SpriteRenderer leftSprite;
    public SpriteRenderer rightSprite;
    private Transform point1;
    private Transform point2;
    public float detectionRadius = 5f;
    public LayerMask detectedPlayer;
    private GameObject player;

    private Vector3 targetPosition;
    private Animator anim;

    public void SetPoints(Transform p1, Transform p2)
    {
        point1 = p1;
        point2 = p2;
        targetPosition = point1.position;
    }

    private void Start()
    {
        slime = GetComponentInParent<Slime>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInParent<Animator>();
    }

    private void FixedUpdate()
    {
        if (point1 == null || point2 == null) return;
        DetectedTarget();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.parent.position, detectionRadius);
    }

    private void DetectedTarget()
    {
        Vector2 dir;
        Collider2D detected = Physics2D.OverlapCircle(transform.parent.position, detectionRadius, detectedPlayer);


        if (detected != null)
            dir = (player.transform.position - transform.parent.position).normalized;

        else
            dir = (targetPosition - transform.parent.position).normalized;


        Move(dir);
    }

    private void Move(Vector2 dir)
    {
        transform.parent.position += slime.characterData.speed * Time.deltaTime * (Vector3)dir;


        if (Vector3.Distance(transform.parent.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == point1.position ? point2.position : point1.position;
        }


        FlipSprite(dir);
    }



    private void FlipSprite(Vector3 dir)
    {
        if (dir.x > 0)
        {
            leftSprite.enabled = false;
            rightSprite.enabled = true;
        }
        else if (dir.x < 0)
        {
            leftSprite.enabled = true;
            rightSprite.enabled = false;
        }
    }
}
