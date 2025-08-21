using UnityEngine;

public class SpawnEnemy : Spawner
{
    public Transform point1;
    public Transform point2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
    }


    protected override void OnOjectSpawned(GameObject spawnedEnemy)
    {
        SlimeEnemyMovement enemyMovement = spawnedEnemy.GetComponentInChildren<SlimeEnemyMovement>();
        enemyMovement.SetPoints(point1, point2);
    }
}
