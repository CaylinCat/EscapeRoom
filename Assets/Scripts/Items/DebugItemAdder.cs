using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adds all the items to the inventory immediately. 
/// Used for testing and debugging purposes.
/// </summary>
public class DebugItemAdder : MonoBehaviour
{
    public List<Item> Items;

    void Start()
    {
        foreach(Item item in Items)
        {
            InventoryManager.Instance.AddItem(item);
        }
    }
}
