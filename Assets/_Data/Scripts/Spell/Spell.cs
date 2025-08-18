using UnityEngine;

public class Spell : MonoBehaviour
{
    public SpellData spellData;

    private int Damage => spellData.damage;

    public virtual void UseSpell(IDamageable target)
    {
        Debug.Log("Target spell: " + target);
        target?.TakeDamage(Damage);
    }
}
