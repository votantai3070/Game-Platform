using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject prefabToSpawn;
    public Transform spawnPoint;

    protected virtual GameObject SpawnPrefab()
    {
        if (prefabToSpawn == null || spawnPoint == null)
        {
            Debug.LogError("Prefab to spawn or spawn point is not set.");
            return null;
        }

        return Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
    }

    protected abstract void OnOjectSpawned(GameObject spawnedObject);

    public void Spawn()
    {
        GameObject spawnedObject = SpawnPrefab();
        if (spawnedObject != null)
        {
            OnOjectSpawned(spawnedObject);
        }
    }
}
