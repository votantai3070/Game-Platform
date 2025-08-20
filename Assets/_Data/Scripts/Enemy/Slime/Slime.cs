public class Slime : Character
{
    protected override void Die()
    {
        Destroy(gameObject);
        HealthPotion.Instance.DropItem(transform);
    }

}
