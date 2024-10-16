using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NRIngredientSlot : MonoBehaviour
{
    public bool IsOccupied = false;
    public int Index;
    public InnerCircle innerCircle;
    private SpriteRenderer slotSprite;


    void Start()
    {
        slotSprite = GetComponent<SpriteRenderer>();
        if (slotSprite == null)
        {
            Debug.LogError("Image component not found on this NRIngredientSlot!");
        }
    }
    public void OnSlotInteract()
    {
        if (IsOccupied) return;
        UpdateSlotImage(InventoryManager.Instance.SelectedItem.GetItemSprite());
        innerCircle.AddItemToSlot(this, InventoryManager.Instance.SelectedItem.GetItemID());
        InventoryManager.Instance.RemoveItem();
        IsOccupied = true;
    }

    private void UpdateSlotImage(Sprite itemSprite)
    {
        if (slotSprite != null)
        {
            slotSprite.sprite = itemSprite; 
            slotSprite.color = Color.white;
        }
        else
        {
            Debug.LogError("No Image component found to update in NRIngredientSlot.");
        }
    }
}