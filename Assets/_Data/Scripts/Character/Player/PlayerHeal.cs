using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    private HealthPotion healthPotion;
    public Player player;

    private void Start()
    {
        healthPotion = FindAnyObjectByType<HealthPotion>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && player.CurrentHealth < player.characterData.maxHealth)
        {
            healthPotion.UseItem();
        }
    }
}
