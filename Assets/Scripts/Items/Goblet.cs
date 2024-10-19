using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblet : Item
{
    public override ItemID GetItemID() { return ItemID.GOBLET; }
    public bool Filled;
    public Item FingerBone;

    protected override void Interact()
    {
        
    }
}
