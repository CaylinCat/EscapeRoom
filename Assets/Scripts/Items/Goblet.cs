using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblet : Item
{
    public override ItemID GetItemID() { return ItemID.GOBLET; }
    public bool Filled;
    public Item FingerBone;

    void Awake()
    {
        if(Filled) {
            ItemInspectOverride = FindObjectsByType<GobletInspector>(FindObjectsInactive.Include, FindObjectsSortMode.None)[0].gameObject;
        }
    }

    protected override void Interact()
    {
        
    }
}
