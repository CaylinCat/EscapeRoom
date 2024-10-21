using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Item SelectedItem;
    public GameObject[] ItemSlots;
    public GameObject SelectionIcon;
    public GameObject HoverIcon;
    public FMODUnity.StudioEventEmitter SFX;
    [HideInInspector] public bool CanHover = true;
    public List<GameObject> heldItems = new List<GameObject>();
    private int currentPage = 0;
    private const int itemsPerPage = 10;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button previousPageButton;
    
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
        UpdateInventoryDisplay();
    }

    public void AddItem(Item item)
    {
        if(heldItems.Count >= ItemSlots.Length * (currentPage + 1))
        {
            // Debug.LogWarning("Item added exceeds the current page capacity. Moving to the next page.");
            currentPage++;
            UpdateInventoryDisplay();
        }

        GameObject itemInstance = Instantiate(item.gameObject);
        heldItems.Add(itemInstance);
        int slotIndex = (heldItems.Count - 1) % itemsPerPage;
        SetItemToSlot(itemInstance, ItemSlots[slotIndex]);
        UpdateInventoryDisplay();
    }

    public void SelectItem(Item item)
    {
        SFX.Play();
        SelectedItem = item;
        SelectionIcon.SetActive(true);
        SelectionIcon.transform.position = item.transform.parent.position;
        UnhoverItem();
    }

    public void HoverItem(Item item)
    {
        if(CanHover)
        SFX.Play();
        HoverIcon.SetActive(true);
        HoverIcon.transform.position = item.transform.parent.position;
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
        UpdateInventoryDisplay();
        if(heldItems.Count <= ItemSlots.Length * currentPage) ChangePage(-1);
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
                UpdateInventoryDisplay();
                if(heldItems.Count <= ItemSlots.Length * currentPage) ChangePage(-1);
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

    private void UpdateInventoryDisplay()
    {
        foreach (GameObject itemObject in heldItems)
        {
            itemObject.SetActive(false);
        }

        int startIndex = currentPage * itemsPerPage;
        int endIndex = Mathf.Min(startIndex + itemsPerPage, heldItems.Count);
        int slotIndex = 0;
        for (int i = startIndex; i < endIndex; i++)
        {
            heldItems[i].SetActive(true);
            SetItemToSlot(heldItems[i], ItemSlots[slotIndex]);
            slotIndex++;
        }

        UpdateButtons();
    }

    public void ChangePage(int direction)
    {
        DeselectItem();
        int maxPage = Mathf.CeilToInt((float)heldItems.Count / itemsPerPage) - 1;
        int newPage = Mathf.Clamp(currentPage + direction, 0, maxPage);

        if (newPage != currentPage || true)
        {
            currentPage = newPage;
            UpdateInventoryDisplay();
        }
    }

    public void UpdateButtons()
    {
        int maxPage = Mathf.Max(0, Mathf.CeilToInt((float)heldItems.Count / itemsPerPage) - 1);

        previousPageButton.gameObject.SetActive(currentPage > 0);
        nextPageButton.gameObject.SetActive(currentPage < maxPage);
    }
}
