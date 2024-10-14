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

public class MusicBoxPin : MonoBehaviour {
    public MusicBoxSlot Slot;
    public PinRowID RowID;

    void Awake()
    {
        if(Slot != null) Slot.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnMouseDown()
    {
        MusicBox.Instance.SelectedPin = this;
        MusicBox.Instance.OldSlot = Slot;
    }

    public void OnMouseDrag()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        MusicBox.Instance.SelectedPin = this;
        MusicBox.Instance.OldSlot = Slot;
    }

    public void OnMouseUp()
    {
        // TODO Check if over slots

        if(Slot != null) transform.position = Slot.transform.position;
        else transform.localPosition = Vector3.zero;
    }
}
