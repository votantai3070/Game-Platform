using System.Collections.Generic;
using UnityEngine;

public class BossSpawnSlimePool : MonoBehaviour
{
    public List<GameObject> minionsPool = new();
    public Transform spawnPoint;
    public GameObject minionPrefab;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject minion = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
            minion.SetActive(false);
            minionsPool.Add(minion);
        }
    }

    public void GetSpawnMinions()
    {
        foreach (var minion in minionsPool)
        {
            if (!minion.activeInHierarchy)
            {
                minion.SetActive(true);
                return;
            }
        }

        GameObject newMinion = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
        newMinion.SetActive(true);
        minionsPool.Add(newMinion);
        return;
    }

    public void ReturnMinionToPool(GameObject minion)
    {
        minion.SetActive(false);
    }
}
