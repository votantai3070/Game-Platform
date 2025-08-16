using UnityEngine;

public class SpawnPlayer : Spawner
{

    private void Start()
    {
        Spawn();
    }

    protected override void OnOjectSpawned(GameObject spawnedObject)
    {
        Debug.Log("spawnedObject: " + spawnedObject);
        GameManager.instance.CameraFollow(spawnedObject);
    }
}
