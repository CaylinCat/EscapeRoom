using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UseItemZone : MonoBehaviour
{
    public List<ItemID> AcceptedItems;
    public UnityEvent OnUse;

    public void OnMouseDown() { TryUseItem(); }

    public void TryUseItem()
    {
        if(InventoryManager.Instance.SelectedItem != null)
        {
            if(AcceptedItems.Contains(InventoryManager.Instance.SelectedItem.GetItemID()))
            {
                OnUse.Invoke();
                InventoryManager.Instance.RemoveItem();
            }
        }
    }
}
