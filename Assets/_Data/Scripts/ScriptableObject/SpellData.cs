using UnityEngine;

[CreateAssetMenu(fileName = "NewSpellData", menuName = "Spell/SpellData")]
public class SpellData : ScriptableObject
{
    public string spellName;
    public int damage;
    public float criticalChance;
    public float criticalMultiplier;
    //public int debuff;
}
