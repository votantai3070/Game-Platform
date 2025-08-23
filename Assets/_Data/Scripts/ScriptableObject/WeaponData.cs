using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Information")]
    public string weaponName;
    public int damage;
    public float criticalChance;
    public float criticalMultiplier;
}
