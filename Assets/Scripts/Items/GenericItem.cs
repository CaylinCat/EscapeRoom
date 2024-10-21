using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericItem : Item
{
    public ItemID Type;

    public override ItemID GetItemID() { return Type; }
    protected override void Interact() { return; }
}
