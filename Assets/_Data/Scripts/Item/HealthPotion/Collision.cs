using UnityEngine;

public class Collision : MonoBehaviour
{
    private Transform player;
    public ItemData healthPotionData;
    private bool playerRange = false;
    private float playerRangeDistance = 1.5f;
    public HealthPotion healthPotion;

    private void Start()
    {
        healthPotion = FindAnyObjectByType<HealthPotion>();
    }

    private void Update()
    {
        PickUpPotion();
    }

    void PickUpPotion()
    {
        if (player == null)
        {
            return;
        }
        float distance = Vector2.Distance(player.position, transform.position);
        Inventory inventory = player.GetComponent<Inventory>();
        if (playerRange && Input.GetKeyDown(KeyCode.E) && playerRangeDistance >= distance)
        {
            healthPotion.TakeItem(gameObject);
            inventory.AddHealthPotion(healthPotionData.itemAdditionalValue);
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
