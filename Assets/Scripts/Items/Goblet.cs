using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblet : Item
{
    public override ItemID GetItemID() { return ItemID.GOBLET; }
    public Image GobletImage;
    public Sprite FilledGobletSprite;
    public bool Filled;

    protected override void Interact()
    {
        Debug.Log("Clicked!");
        // TODO: remove debug
        GobletImage.sprite = FilledGobletSprite;
    }
}
