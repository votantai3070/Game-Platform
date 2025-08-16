using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CinemachineCamera cinemachine;

    private void Awake()
    {
        instance = this;
    }

    public void CameraFollow(GameObject playerPrefab)
    {
        cinemachine.Follow = playerPrefab.transform;
    }
}
