using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkullPuzzle : Puzzle
{
    public Item Rook;
    public SpriteRenderer SkullSR;
    public Sprite MissingRookSprite;
    private bool complete = false;

    public void GrabRook()
    {
        if(complete) return;

        InventoryManager.Instance.AddItem(Rook);
        SkullSR.sprite = MissingRookSprite;
        complete = true;
        OnComplete();
    }
}
