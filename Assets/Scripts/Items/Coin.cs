using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public override ItemID GetItemID() { return ItemID.NR_CLUE_3; }

    void Awake()
    {
        ItemInspectOverride = FindObjectsByType<CoinInspector>(FindObjectsInactive.Include, FindObjectsSortMode.None)[0].gameObject;
    }

    protected override void Interact()
    {
        
    }
}
