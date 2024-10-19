using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public Item Item;

    void OnMouseDown()
    {
        InventoryManager.Instance.AddItem(Item);
        Destroy(this.gameObject);
    }
}
