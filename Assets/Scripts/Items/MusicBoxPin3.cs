using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxPin3 : Item
{
    public override ItemID GetItemID() { return ItemID.MUSICBOX_PIN_3; }

    protected override void Interact()
    {
        Debug.Log("Clicked!");
    }

    void Start()
    {
        inInventory = true;
    }
}
