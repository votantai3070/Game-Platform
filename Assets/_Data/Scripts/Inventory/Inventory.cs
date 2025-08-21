using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour, IInventory
{
    public ItemData healthPotionData;
    public TextMeshProUGUI healthPotionText;
    public int HealthPotions { get; set; }
    public int MaxHealthPotions { get; set; }

    private void Start()
    {
        HealthPotions = healthPotionData.defaultStackSize;
        MaxHealthPotions = healthPotionData.maxStackSize;
        if (healthPotionText != null)
        {
            healthPotionText.text = HealthPotions.ToString();
        }
    }

    private void Update()
    {
        UpdateHealthPotionText();
    }

    void UpdateHealthPotionText()
    {
        if (healthPotionText != null)
        {
            healthPotionText.text = HealthPotions.ToString();
        }
    }

    public void AddHealthPotion(int amount)
    {
        HealthPotions += amount;
        if (HealthPotions > MaxHealthPotions)
        {
            HealthPotions = MaxHealthPotions;
        }
    }




}
