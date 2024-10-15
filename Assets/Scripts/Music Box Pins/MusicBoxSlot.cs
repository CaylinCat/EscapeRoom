using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MusicBoxSlot : MonoBehaviour
{
    public List<PinRowID> AcceptedItems;
    public bool IsFilled;

    void OnMouseDown() { TryPlacePin(); }

    public void TryPlacePin()
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
