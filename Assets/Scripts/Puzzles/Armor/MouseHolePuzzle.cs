using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHolePuzzle : Puzzle
{
    [SerializeField] private GameObject tooth;
    [SerializeField] private Item toothItem;
    public void EnableTooth()
    {
        tooth.SetActive(true);
    }

    public void GiveTooth()
    {
        InventoryManager.Instance.AddItem(toothItem);
        tooth.SetActive(false);
        ArmorPuzzle.progress++;
        OnComplete();

    }
}
