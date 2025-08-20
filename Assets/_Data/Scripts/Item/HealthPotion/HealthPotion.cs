using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IItem
{
    public static HealthPotion Instance { get; private set; }
    public GameObject healthPotionPrefab;

    private bool isAnimating = false;

    private void Awake()
    {
        Instance = this;
    }


    public void DropItem(Transform target)
    {
        if (target == null)
        {
            return;
        }
        GameObject potion = Instantiate(healthPotionPrefab, target.position, Quaternion.identity);
        potion.transform.position = new Vector2(target.position.x, target.position.y + 1f);
        StartCoroutine(AnimationPotion(potion));
    }


    public void TakeItem(GameObject gameObject)
    {
        PickupUI.Instance.HidePickupMessage();
        Destroy(gameObject);
    }

    public void UseItem()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator AnimationPotion(GameObject potion)
    {
        isAnimating = true;
        Vector2 startPosition = potion.transform.position;
        Vector2 endPosition = new Vector2(startPosition.x, startPosition.y + 0.5f);
        float elapsedTime = 0f;
        float duration = 0.5f;
        while (elapsedTime < duration)
        {
            potion.transform.position = Vector2.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isAnimating = false;
    }
}
