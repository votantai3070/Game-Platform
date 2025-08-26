using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Info")]
    private Character owner;
    public WeaponData weaponData;
    private int currentDamage;
    public int Damage => weaponData.damage;
    private bool isCrit;


    private void CalculateDamage()
    {
        Debug.Log("owner: " + owner);
        if (owner == null)
        {
            Debug.LogWarning("Owner is not set for the weapon. Cannot calculate damage.");
            return;
        }
        isCrit = Random.value < weaponData.criticalChance;
        currentDamage = Damage + owner.Damage;
        if (isCrit)
        {
            currentDamage = Mathf.RoundToInt(currentDamage * weaponData.criticalMultiplier);
        }
        Debug.Log($"Calculated Damage: {currentDamage} (Crit: {isCrit})");
    }

    public void Init(Character characterOwner)
    {
        owner = characterOwner;
    }

    public virtual void UseWeapon(IDamageable target)
    {
        Debug.Log("target: " + target);
        if (target == null || owner == null) return;
        Debug.Log("Using weapon on target.");
        CalculateDamage();
        target.TakeDamage(currentDamage, isCrit);
    }
}
