using UnityEngine;
using UnityEngine.EventSystems;


public enum ItemID
{
    NONE,
    TEST_ITEM
}

/// <summary>
/// The item superclass. Items are interactable objects that can
/// be added to the inventory.
/// </summary>
public abstract class Item : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    protected bool inInventory;

    /// <summary>
    /// Called whenever the item is clicked on
    /// </summary>
    protected abstract void Interact();
    public abstract ItemID GetItemID();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inInventory)
        {
            InventoryManager.Instance.SelectedItem = this;
        }

        Interact();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(inInventory)
        {
            InventoryManager.Instance.SelectedItem = this;
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }
}
