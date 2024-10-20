using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ItemID
{
    NONE,
    TEST_ITEM,
    TEST_ITEM_2,
    TEST_ITEM_3,
    ROOK,
    STAMP,
    MUSICBOX_PIN_1,
    MUSICBOX_PIN_2,
    MUSICBOX_PIN_3,
    MUSICBOX_PIN_4,
    MUSICBOX_PIN_5,
    AMULET,
    DRAGON_TOOTH,
    HAIR,
    MIRROR,
    HEART,
    FINGER_BONE,
    RING,
    PHOTOGRAPH,
    TORCH,
    BIRDSEED,
    LIZARD_LEG,
    MANDRAKE,
    EYEBALL,
    GOBLET,
    CHESS_SCROLL,
    WAX,
    ENVELOPE,
    MAP_KEY,
    LETTER_NAMED,
    NR_CLUE_3,
    GEMSTONE,
    NR_CLUE_6,
    NR_CLUE_7,
    BRICKS_CLUE,
    NR_CLUE_0,
}

/// <summary>
/// The item superclass. Items are objects in the inventory.
/// </summary>
public abstract class Item : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite itemSprite;
    public GameObject ItemInspectOverride;
    
    /// <summary>
    /// Called whenever the item is clicked on
    /// </summary>
    protected abstract void Interact();
    public abstract ItemID GetItemID();

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.clickCount == 1) 
        {
            if(ItemInspectOverride == null) PuzzleManager.Instance.InspectGenericItem(this);
            else PuzzleManager.Instance.ShowPuzzle(ItemInspectOverride); 
            return;
        }

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

    public Sprite GetItemSprite()
    {
        if(itemSprite != null) return itemSprite;
        
        foreach(Transform child in transform)
        {
            Image itemImage = child.GetComponent<Image>();
            if(itemImage != null) return itemImage.sprite;
        }
        
        Debug.LogWarning("No sprite was found for this Item!");
        return null;
    }
}
