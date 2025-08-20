using UnityEngine;

public interface IItem
{
    void UseItem();
    void DropItem(Transform transform);
    void TakeItem(GameObject gameObject);
}
