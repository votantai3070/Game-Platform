using UnityEngine;

public class ReCallSlimeEnemy : MonoBehaviour
{
    private Transform player;
    public float recallSpeed = 5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MoveOnPlayer();
    }

    private void MoveOnPlayer()
    {
        Vector2 direction = (player.position - transform.parent.position).normalized;

        transform.parent.position += recallSpeed * Time.deltaTime * (Vector3)direction;
    }
}
