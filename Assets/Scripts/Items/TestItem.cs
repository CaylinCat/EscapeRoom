using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item
{
    public override ItemID GetItemID() { return ItemID.TEST_ITEM; }

    protected override void Interact()
    {
        Debug.Log("Clicked!");
    }

    void Start()
    {
        inInventory = true;
    }
}
