using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CagePuzzle : Puzzle
{
    [SerializeField] private GameObject eyeball;
    [SerializeField] private Item eyeballItem;
    public void EnableEyeball()
    {
        eyeball.SetActive(true);
    }

    public void GiveEyeball()
    {
        InventoryManager.Instance.AddItem(eyeballItem);
        eyeball.SetActive(false);
        ArmorPuzzle.progress++;
        OnComplete();

    }
}
