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
    public int defaultStackSize = 3;
    public int maxStackSize = 5;
    public int itemAdditionalValue = 1;
    public GameObject itemPrefab;
}
