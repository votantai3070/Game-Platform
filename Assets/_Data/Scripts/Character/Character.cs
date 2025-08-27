using DG.Tweening;
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
    private DamagePopup damagePopup;

    public int CurrentHealth { get; set; }
    public int MaxHealth { get; private set; }

    public int Damage => characterData.damage;

    protected virtual void Start()
    {
        Debug.Log($"Character {gameObject.name} Start method called.");
        if (characterData == null)
        {
            return;
        }

        damagePopup = FindAnyObjectByType<DamagePopup>();

        MaxHealth = characterData.maxHealth;

        CurrentHealth = MaxHealth;

        Debug.Log($"Character {gameObject.name} initialized with {CurrentHealth}/{MaxHealth} health.");


        if (characterHealthBar != null)
        {
            characterHealthBar.maxValue = characterData.maxHealth;
            characterHealthBar.value = CurrentHealth;
        }

    }

    private void Update()
    {
        Debug.Log($"{gameObject.name} | Health: {CurrentHealth}");
    }

    protected virtual void Move(Vector2 direction) { }


    protected virtual void Attack(IDamageable target) { }

    protected virtual void CalculateDamage() { }

    public void TakeDamage(int damage, bool isCrit)
    {
        CurrentHealth -= damage;

        if (characterHealthBar != null)
        {
            characterHealthBar.value = CurrentHealth;
        }

        if (damagePopup != null)
        {
            damagePopup.ShowDamage(transform.position, damage, isCrit);
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, characterData.maxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, characterData.maxHealth);

        if (characterHealthBar != null)
        {
            DOTween.Kill(characterHealthBar);
            DOTween.To(() => characterHealthBar.value, x => characterHealthBar.value = x, CurrentHealth, 0.5f);
        }

    }

    protected abstract void Die();

}
