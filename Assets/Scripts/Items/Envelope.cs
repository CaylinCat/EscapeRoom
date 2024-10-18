using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Envelope : Item
{
    public override ItemID GetItemID() { return ItemID.ENVELOPE; }
    public bool Sealed;
    public GameObject Seal;

    protected override void Interact()
    {
        if(Sealed)
        {
            // InventoryManager.Instance.RemoveItem();
            // InventoryManager.Instance.AddItem(FingerBone);
        }
    }
}
