using UnityEngine;
using UnityEngine.EventSystems;


public enum ItemID
{
    NONE,
    TEST_ITEM,
    TEST_ITEM_2,
    TEST_ITEM_3,
    ROOK,
    MANDRAKE,
    STAMP,
    MUSICBOX_PIN_1,
    MUSICBOX_PIN_2,
    MUSICBOX_PIN_3,
    MUSICBOX_PIN_4,
    MUSICBOX_PIN_5,
}

/// <summary>
/// The item superclass. Items are objects in the inventory.
/// </summary>
public abstract class Item : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Called whenever the item is clicked on
    /// </summary>
    protected abstract void Interact();
    public abstract ItemID GetItemID();

    public void OnPointerDown(PointerEventData eventData)
    {
        if(InventoryManager.Instance.SelectedItem != this) 
        {
            InventoryManager.Instance.SelectItem(this);
            Interact();
        }
        else InventoryManager.Instance.DeselectItem();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.CanHover = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check for interactions in world space rather than UI space
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos, 1f);
        foreach(Collider2D collider in colliders)
        {
            UseItemZone zone = collider.gameObject.GetComponent<UseItemZone>();
            if(zone != null) zone.TryUseItem();
        }

        InventoryManager.Instance.DeselectItem();
        InventoryManager.Instance.CanHover = true;
        transform.localPosition = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(InventoryManager.Instance.SelectedItem != this) InventoryManager.Instance.HoverItem(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.UnhoverItem();
    }
}
