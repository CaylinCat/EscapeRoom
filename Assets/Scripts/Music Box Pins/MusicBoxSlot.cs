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
        if(MusicBox.Instance.SelectedPin != null)
        {
            if(AcceptedItems.Contains(MusicBox.Instance.SelectedPin.RowID))
            {
                MusicBox.Instance.PlacePin(this);
            }
        }
    }
}
