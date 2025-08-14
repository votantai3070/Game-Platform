using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Info")]
    private Character owner;
    public WeaponData weaponData;
    private int currentDamage;
    public int Damage => weaponData.damage;

    private void Update()
    {
        CalculateDamage();
    }
    private void CalculateDamage()
    {
        currentDamage = Damage + owner.Damage;
    }

    public void Init(Character characterOwner)
    {
        Debug.Log($"Initializing weapon: {weaponData.weaponName} for character: {characterOwner.characterData.characterName}");
        owner = characterOwner;
    }

    public virtual void UseWeapon(IDamageable target)
    {
        Debug.Log($"{owner.characterData.characterName} is using {weaponData.weaponName} on {target?.GetType().Name}");
        if (target == null || owner == null) return;
        // Check if the target is within attack range
        target.TakeDamage(currentDamage);
    }
}
