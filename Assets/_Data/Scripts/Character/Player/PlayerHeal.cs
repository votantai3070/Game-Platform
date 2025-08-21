using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    private HealthPotion healthPotion;

    private void Start()
    {
        healthPotion = FindAnyObjectByType<HealthPotion>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            healthPotion.UseItem();
        }
    }
}
