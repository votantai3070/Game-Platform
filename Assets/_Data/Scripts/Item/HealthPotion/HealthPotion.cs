using DG.Tweening;
using System.Collections;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IItem
{
    public GameObject healthPotionPrefab;
    private Inventory inventory;
    private Player player;


    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        player = GameObject.Find("Player").GetComponent<Player>();
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
        if (inventory.HealthPotions <= 0)
            return;

        inventory.HealthPotions--;

        if (inventory.healthPotionText != null)
            inventory.healthPotionText.text = inventory.HealthPotions.ToString();

        if (player != null)
            player.Heal(inventory.healthPotionData.itemValue);
    }

    IEnumerator AnimationPotion(GameObject potion)
    {
        //Vector2 startPosition = potion.transform.position;
        //Vector2 endPosition = new Vector2(startPosition.x, startPosition.y + 0.5f);
        float elapsedTime = 0f;
        float duration = 0.5f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            DOTween.Kill(potion);
            potion.transform.DOMoveY(potion.transform.position.y + 0.2f, duration).SetEase(Ease.OutQuad);
            yield return null;
        }
    }
}
