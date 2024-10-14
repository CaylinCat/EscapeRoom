using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotWidthManager : MonoBehaviour
{
    public Transform[] slots;
    public GameObject[] books;

    void Start()
    {
        AdjustSlotWidths();
    }

    void AdjustSlotWidths()
    {
        for (int i = 0; i < books.Length; i++)
        {
            SpriteRenderer bookSpriteRenderer = books[i].GetComponent<SpriteRenderer>();
            if (bookSpriteRenderer != null)
            {
                float bookWidth = bookSpriteRenderer.bounds.size.x;
                AdjustSlotWidth(slots[i], bookWidth);
            }
        }
    }

    void AdjustSlotWidth(Transform slot, float bookWidth)
    {
        RectTransform slotRectTransform = slot.GetComponent<RectTransform>();
        if (slotRectTransform != null)
        {
            Vector2 newSize = new Vector2(bookWidth, slotRectTransform.sizeDelta.y);
            slotRectTransform.sizeDelta = newSize;
        }
        else
        {
            slot.localScale = new Vector3(bookWidth, slot.localScale.y, slot.localScale.z);
        }
    }
}