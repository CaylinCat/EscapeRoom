using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public ItemID SelectedItem;
    
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
        SelectedItem = ItemID.NONE;
    }
}
