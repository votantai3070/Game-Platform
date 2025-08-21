public class Slime : Character
{
    public HealthPotion healthPotion;

    protected override void Start()
    {
        healthPotion = FindAnyObjectByType<HealthPotion>();
    }

    protected override void Die()
    {
        Destroy(gameObject);
        healthPotion.DropItem(transform);
    }

}
