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

    // The functionality for using items should be located in the corresponding puzzle in practice
    public void TestUseItem()
    {
        Debug.Log("Item Used");
        Destroy(gameObject);
    }
}
