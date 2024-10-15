using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandelabraPuzzle : Puzzle
{
    public Item NRClue5;
    public SpriteRenderer CandelabraImage;
    public Sprite LitUpCanelabra;
    private bool complete = false;

    public void LitCandles()
    {
        if(complete) return;

        InventoryManager.Instance.AddItem(NRClue5);
        CandelabraImage.sprite = LitUpCanelabra;
        complete = true;
        OnComplete();
    }
}
