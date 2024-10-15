using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPuzzle : Puzzle
{
    public Item Ring;
    private bool complete = false;

    public void GainRing()
    {
        if(complete) return;

        InventoryManager.Instance.AddItem(Ring);
        complete = true;
        OnComplete();
    }
}
