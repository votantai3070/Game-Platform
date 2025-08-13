using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform point1;
    public Transform point2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
    }


    private void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyMovement enemyMovement = enemy.GetComponentInChildren<EnemyMovement>();
        enemyMovement.SetPoints(point1, point2);
    }
}
