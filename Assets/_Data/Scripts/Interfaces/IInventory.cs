public interface IInventory
{
    int HealthPotions { get; set; }
    int MaxHealthPotions { get; set; }
    void AddHealthPotion(int amount);
}
