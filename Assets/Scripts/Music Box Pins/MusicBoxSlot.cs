using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MusicBoxSlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public List<PinRowID> AcceptedItems;
    public bool IsFilled;

    public void OnPointerClick(PointerEventData eventData) { TryPlacePin(); }
    public void OnDrop(PointerEventData eventData) { TryPlacePin(); }

    private void TryPlacePin()
    {
        Debug.Log($"Try placing pin at {gameObject.name}");
        if(MusicBox.Instance.SelectedPin != null)
        {
            Debug.Log($"SUCCESS Placed Pin at {gameObject.name}");
            // if(AcceptedItems.Contains(MusicBox.Instance.SelectedPin.GetPinRowID()))
            if(AcceptedItems.Contains(MusicBox.Instance.SelectedPin.RowID))
            {
                MusicBox.Instance.PlacePin(this);
            }
        }
    }
}
