using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform bossRoom;
    public Slider bossHp;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;

    private void Awake()
    {
        bossHp.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        CamFollow();
    }

    private void CamFollow()
    {
        if (bossRoom != null && Vector3.Distance(player.position, bossRoom.position) < 30f)
        {
            CamFollowBossRoom();
        }
        else
        {
            CamFollowPlayer();
        }
    }

    private void CamFollowBossRoom()
    {

        Vector3 targetPos = new(bossRoom.position.x, bossRoom.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        bossHp.gameObject.SetActive(true);
    }

    private void CamFollowPlayer()
    {
        Vector3 targetPosition = new(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        bossHp.gameObject.SetActive(false);
    }
}
