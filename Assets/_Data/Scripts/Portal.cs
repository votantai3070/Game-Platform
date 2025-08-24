using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportTarget;
    private bool isTeleport = true;

    public float teleportDelay = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTeleport)
        {
            StartCoroutine(TeleportPlayer(collision.transform));
        }
    }

    IEnumerator TeleportPlayer(Transform player)
    {
        Portal portalScript = teleportTarget.GetComponent<Portal>();
        isTeleport = false;
        portalScript.isTeleport = false;

        player.position = teleportTarget.position;

        yield return new WaitForSeconds(teleportDelay);

        ResetTeleport();
        portalScript.ResetTeleport();
    }

    private void ResetTeleport()
    {
        isTeleport = true;
    }
}
