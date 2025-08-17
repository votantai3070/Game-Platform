using UnityEngine;

public class Spell : MonoBehaviour
{
    private Character owner;
    public SpellData spellData;

    private int Damage => spellData.damage;

    public void Init(Character character)
    {
        if (character == null)
            owner = character;
    }

    public virtual void UseSpell(IDamageable target)
    {
        Debug.Log($"{owner.characterData.characterName} is using {spellData.spellName} on {target?.GetType().Name}");
        if (target == null || owner == null) return;
        // Check if the target is within attack range
        target.TakeDamage(Damage);
    }
}
