using System.Collections;
using UnityEngine;

public class TeleportFader : MonoBehaviour
{
    public CanvasGroup fadePanel;

    public IEnumerator FadeOut(float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(0, 1, t / duration);
            yield return null;
        }
    }

    public IEnumerator FadeIn(float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(1, 0, t / duration);
            yield return null;
        }
    }
}
