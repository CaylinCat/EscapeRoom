using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Item SelectedItem;
    public GameObject[] ItemSlots;
    public GameObject SelectionIcon;
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
        SelectedItem = item;
        SelectionIcon.SetActive(true);
        SelectionIcon.transform.SetParent(item.transform.parent);
        SelectionIcon.transform.localPosition = Vector3.zero;
    }

    public void DeselectItem()
    {
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
        
        // Reorganize inventory
        // TODO account for multiple pages of items
        int slotIndex = 0;
        foreach(GameObject itemObject in heldItems)
        {
            SetItemToSlot(itemObject, ItemSlots[slotIndex]);
            slotIndex++;
        }
    }

    private void SetItemToSlot(GameObject item, GameObject slot)
    {
        item.transform.SetParent(slot.transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
    }
}
