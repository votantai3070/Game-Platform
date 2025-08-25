using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform teleportTarget;
    public TeleportFader fader;
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
        StartCoroutine(fader.FadeOut(teleportDelay / 2));

        player.position = teleportTarget.position;

        yield return new WaitForSeconds(teleportDelay);

        StartCoroutine(fader.FadeIn(teleportDelay / 2));

        ResetTeleport();
        portalScript.ResetTeleport();
    }

    private void ResetTeleport()
    {
        isTeleport = true;
    }
}
