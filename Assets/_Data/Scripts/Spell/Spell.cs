using UnityEngine;

public class Spell : MonoBehaviour
{
    public SpellData spellData;
    private bool isCrit;

    private int Damage => spellData.damage;
    private int currentDamage;

    private void CalculateDamage()
    {
        isCrit = Random.value < spellData.criticalChance;
        currentDamage = Damage;
        if (isCrit)
        {
            currentDamage = Mathf.RoundToInt(currentDamage * spellData.criticalMultiplier);
        }
    }

    public virtual void UseSpell(IDamageable target)
    {
        CalculateDamage();
        target?.TakeDamage(currentDamage, isCrit);
    }
}
