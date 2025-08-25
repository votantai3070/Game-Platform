public class Slime : Character
{
    public HealthPotion healthPotion;

    private BossSpawnSlimePool bossSpawnSlimePool;

    protected override void Start()
    {
        healthPotion = FindAnyObjectByType<HealthPotion>();
        bossSpawnSlimePool = FindAnyObjectByType<BossSpawnSlimePool>();
    }

    protected override void Die()
    {
        bossSpawnSlimePool.ReturnMinionToPool(gameObject);
        healthPotion.DropItem(transform);
    }

}
