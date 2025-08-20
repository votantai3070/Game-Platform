using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public int maxHealth;
    public int damage;
    public float speed;
    public int stamina;
    public int dogdeStamina;
    public int blockStamina;
    public int jumpStamina;
}
