using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    private HealthPotion healthPotion;
    private Player player;

    private void Start()
    {
        healthPotion = FindAnyObjectByType<HealthPotion>();
        player = GetComponentInParent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && player.CurrentHealth < player.characterData.maxHealth)
        {
            healthPotion.UseItem();
        }
    }
}
