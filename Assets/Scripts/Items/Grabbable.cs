using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public Item Item;
    public SpriteRenderer ItemSpriteRenderer;
    public Sprite NewSprite;

    void OnMouseDown()
    {
        ItemSpriteRenderer.sprite = NewSprite;
        InventoryManager.Instance.AddItem(Item);
        Destroy(this.gameObject);
    }
}
