using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
* Add items to inventory and removes on screen item image; for items without puzzles
*/

public class AddPuzzlelessItem : MonoBehaviour
{
    public Item PuzzlelessItem;
    void OnMouseDown() { 
        InventoryManager.Instance.AddItem(PuzzlelessItem);
        gameObject.SetActive(false);
    }
}