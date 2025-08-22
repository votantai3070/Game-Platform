using UnityEngine;

public class SpawnBoss : Spawner
{
    public Transform player;
    public Transform bossRoom;

    private bool isBossSpawned = false;

    private void Update()
    {
        SpawnBossInBossRoom();
    }

    private void SpawnBossInBossRoom()
    {
        if (!isBossSpawned && Vector3.Distance(player.position, bossRoom.position) < 60f)
        {
            Spawn();
            isBossSpawned = true;
        }
    }
}
