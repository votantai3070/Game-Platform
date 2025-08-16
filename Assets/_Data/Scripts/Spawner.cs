using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    protected GameObject spawner;

    protected virtual GameObject SpawnPrefab()
    {
        if (prefabToSpawn == null || spawnPoint == null)
        {
            Debug.LogError("Prefab to spawn or spawn point is not set.");
            return null;
        }

        return spawner = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
    }


    protected virtual void OnOjectSpawned(GameObject spawnedObject) { }

    protected void Spawn()
    {
        GameObject spawnedObject = SpawnPrefab();
        if (spawnedObject != null)
        {
            OnOjectSpawned(spawnedObject);
        }
    }
}
