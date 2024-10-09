using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UseItemZone : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public List<ItemID> AcceptedItems;
    public UnityEvent OnUse;

    public void OnPointerClick(PointerEventData eventData) { TryUseItem(); }
    public void OnDrop(PointerEventData eventData) { TryUseItem(); }

    private void TryUseItem()
    {
        if(InventoryManager.Instance.SelectedItem != null)
        {
            if(AcceptedItems.Contains(InventoryManager.Instance.SelectedItem.GetItemID()))
            {
                OnUse.Invoke();
                InventoryManager.Instance.SelectedItem = null;
            }
        }
    }
}
