using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum PinRowID
{
    PIN_ROW_1,
    PIN_ROW_2,
    PIN_ROW_3,
    PIN_ROW_4,
    PIN_ROW_5
}

public class MusicBoxPin : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    public MusicBoxSlot Slot;
    public PinRowID RowID;

    void Awake()
    {
        if(Slot != null) Slot.GetComponent<Image>().enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MusicBox.Instance.SelectedPin = this;
        MusicBox.Instance.OldSlot = Slot;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        MusicBox.Instance.SelectedPin = this;
        MusicBox.Instance.OldSlot = Slot;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Slot != null) transform.position = Slot.transform.position;
        else transform.localPosition = Vector3.zero;
    }
}
