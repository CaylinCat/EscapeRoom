using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Item SelectedItem;
    public GameObject[] ItemSlots;
    public GameObject SelectionIcon;
    public GameObject HoverIcon;
    public FMODUnity.StudioEventEmitter SFX;
    [HideInInspector] public bool CanHover = true;
    private List<GameObject> heldItems = new List<GameObject>();
    
    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the InventoryManager singleton!");
            Destroy(this);
        }
    }

    void Start()
    {
        SelectionIcon.SetActive(false);
        HoverIcon.SetActive(false);
    }

    public void AddItem(Item item)
    {
        if(heldItems.Count + 1 >= ItemSlots.Length)
        {
            Debug.LogWarning("Could not add item, will exceed item slot capacity");
            return;
            // TODO add second page of items
        }

        GameObject itemInstance = Instantiate(item.gameObject);
        heldItems.Add(itemInstance);
        SetItemToSlot(itemInstance, ItemSlots[heldItems.Count-1]);
    }

    public void SelectItem(Item item)
    {
        SFX.Play();
        SelectedItem = item;
        SelectionIcon.SetActive(true);
        SelectionIcon.transform.SetParent(item.transform.parent);
        SelectionIcon.transform.localPosition = Vector3.zero;
        UnhoverItem();
    }

    public void HoverItem(Item item)
    {
        if(CanHover)
        SFX.Play();
        HoverIcon.SetActive(true);
        HoverIcon.transform.SetParent(item.transform.parent);
        HoverIcon.transform.localPosition = Vector3.zero;
    }

    public void UnhoverItem()
    {
        HoverIcon.SetActive(false);
    }

    public void DeselectItem()
    {
        SFX.Play();
        SelectedItem = null;
        SelectionIcon.SetActive(false);
    }

    /// <summary>
    /// Removes the selected item and reorganizes the inventory
    /// </summary>
    public void RemoveItem()
    {
        Item item = SelectedItem;
        DeselectItem();
        heldItems.Remove(item.gameObject);
        Destroy(item.gameObject);
        ReorganizeInventory();
    }

    /// <summary>
    /// Removes the first instance of an item with the matching ID.
    /// If no items match then nothing happens and a warning is logged.
    /// </summary>
    /// <param name="id"></param>
    public void RemoveItemByID(ItemID id)
    {
        foreach(GameObject itemObject in heldItems)
        {
            Item item = itemObject.GetComponent<Item>();
            if(item != null && item.GetItemID() == id)
            {
                DeselectItem();
                heldItems.Remove(item.gameObject);
                Destroy(item.gameObject);
                ReorganizeInventory();
                return;
            }
        }

        Debug.LogWarning($"Trying to remove item with the ID {id} but no match was found!");
    }

    private void SetItemToSlot(GameObject item, GameObject slot)
    {
        item.transform.SetParent(slot.transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
    }

    private void ReorganizeInventory()
    {
        // Reorganize inventory
        // TODO account for multiple pages of items
        int slotIndex = 0;
        foreach(GameObject itemObject in heldItems)
        {
            SetItemToSlot(itemObject, ItemSlots[slotIndex]);
            slotIndex++;
        }
    }
}
