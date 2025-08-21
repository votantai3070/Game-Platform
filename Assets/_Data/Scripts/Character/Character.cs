using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour, IDamageable, IHealth
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
    public Slider characterHealthBar;

    public int CurrentHealth { get; set; }
    public int MaxHealth { get; private set; }

    public int Damage => characterData.damage;

    protected virtual void Start()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterData is not assigned for " + gameObject.name);
            return;
        }

        MaxHealth = characterData.maxHealth;

        CurrentHealth = MaxHealth;


        if (characterHealthBar != null)
        {
            characterHealthBar.maxValue = characterData.maxHealth;
            characterHealthBar.value = CurrentHealth;
        }

    }

    protected virtual void Move(Vector2 direction) { }


    protected virtual void Attack(IDamageable target) { }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (characterHealthBar != null)
        {
            characterHealthBar.value = CurrentHealth;
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, characterData.maxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if (characterHealthBar != null)
        {
            characterHealthBar.value = CurrentHealth;
        }
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, characterData.maxHealth);
    }

    protected abstract void Die();

}
