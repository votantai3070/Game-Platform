using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    //public int itemID;
    public int itemValue;
    public ItemType itemType;
    public enum ItemType
    {
        Consumable,
        Equipment,
        Material,
        QuestItem,
        Miscellaneous
    }
    public bool isStackable;
    public int maxStackSize = 99;
    public GameObject itemPrefab;
}
