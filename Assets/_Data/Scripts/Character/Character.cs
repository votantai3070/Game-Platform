using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public enum CharacterType
    {
        Player,
        Enemy,
        NPC
    }

    [Header("Character Info")]
    public CharacterData characterData;
    public CharacterType characterType;
    protected int currentHealth;

    public int Damage => characterData.damage;

    protected virtual void Start()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterData is not assigned for " + gameObject.name);
            return;
        }
        currentHealth = characterData.maxHealth;
    }

    protected virtual void Move(Vector2 direction) { }


    protected virtual void Attack(IDamageable target) { }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log($"{characterData.characterName} took {damage} damage. Current health: {currentHealth}");

        currentHealth = Mathf.Clamp(currentHealth, 0, characterData.maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
