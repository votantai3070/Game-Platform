using System.Collections;
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

        StartCoroutine(SetFollowNextFrame(playerPrefab));
    }

    private IEnumerator SetFollowNextFrame(GameObject playerPrefab)
    {
        yield return null; // chờ 1 frame cho chắc
        if (cinemachine != null)
        {
            cinemachine.Follow = playerPrefab.transform; // 👈 gán ở đây
        }
        else
        {
            Debug.LogError("CinemachineFollow component not found on the camera!");
        }
    }
}
