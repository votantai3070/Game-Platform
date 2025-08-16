using UnityEngine;

public class SpawnPlayer : Spawner
{

    private void Start()
    {
        Spawn();
        GameManager.instance.CameraFollow(spawner);
    }
}
