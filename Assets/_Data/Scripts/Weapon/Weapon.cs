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
    }

    public void Init(Character characterOwner)
    {
        owner = characterOwner;
    }

    public virtual void UseWeapon(IDamageable target)
    {
        if (target == null || owner == null) return;
        CalculateDamage();
        target.TakeDamage(currentDamage, isCrit);
    }
}
