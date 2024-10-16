using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronPuzzle : Puzzle
{
    public Item Goblet;
    public SpriteRenderer CauldronSR;
    public Sprite CompleteCauldronSprite;
    private bool complete = false;

    public void AddItem()
    {
        
    }

    public void GrabRook()
    {
        if(complete) return;

        InventoryManager.Instance.AddItem(Goblet);
        CauldronSR.sprite = CompleteCauldronSprite;
        complete = true;
        OnComplete();
    }
}
