using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour, IDamageable
{
    public enum CharacterType
    {
        Player,
        Enemy,
        NPC,
        Bosses,
    }

    [Header("Character Info")]
    public CharacterData characterData;
    public CharacterType characterType;
    protected int currentHealth;
    public Slider characterHealthBar;

    public int Damage => characterData.damage;

    protected virtual void Start()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterData is not assigned for " + gameObject.name);
            return;
        }
        currentHealth = characterData.maxHealth;
        if (characterHealthBar != null)
        {
            characterHealthBar.maxValue = characterData.maxHealth;
            characterHealthBar.value = currentHealth;
        }
    }

    protected virtual void Move(Vector2 direction) { }


    protected virtual void Attack(IDamageable target) { }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (characterHealthBar != null)
        {
            characterHealthBar.value = currentHealth;
        }

        Debug.Log($"{characterData.characterName} took {damage} damage. Current health: {currentHealth}");

        currentHealth = Mathf.Clamp(currentHealth, 0, characterData.maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
