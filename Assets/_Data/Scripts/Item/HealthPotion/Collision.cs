using UnityEngine;

public class Collision : MonoBehaviour
{
    private Transform player;
    private bool playerRange = false;
    private float playerRangeDistance = 1.5f;

    private void Update()
    {
        if (player == null)
        {
            return;
        }
        float distance = Vector2.Distance(player.position, transform.position);
        Debug.Log($"Distance to player: {distance}");
        if (playerRange && Input.GetKeyDown(KeyCode.E) && playerRangeDistance >= distance)
        {
            HealthPotion.Instance.TakeItem(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRange = true;
            player = collision.transform;
            PickupUI.Instance.ShowPickupMessage("Press E to pick up Health Potion");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRange = false;
            player = null;
            PickupUI.Instance.HidePickupMessage();
        }
    }
}
