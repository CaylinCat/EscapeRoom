using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedCandlePuzzle : Puzzle
{
    public Item Wax;
    public SpriteRenderer spriteRenderer;
    public Sprite litCandleSprite;
    private bool complete = false;

    public void GrabWax()
    {
        if(complete) return;

        InventoryManager.Instance.AddItem(Wax);
        complete = true;
        OnComplete();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "torch")
        {
            spriteRenderer.sprite = litCandleSprite;
        }
    }
}
