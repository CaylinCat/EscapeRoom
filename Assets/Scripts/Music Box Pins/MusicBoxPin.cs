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
    public SpriteRenderer PinSR;

    void Awake()
    {
        if(Slot != null) Slot.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnMouseDown()
    {
        SelectThis();
        MusicBox.Instance.OldSlot = Slot;
    }

    public void OnMouseDrag()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        MusicBox.Instance.OldSlot = Slot;
    }

    public void OnMouseUp()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // TODO Check if over slots
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos, 0.01f);
        foreach(Collider2D collider in colliders)
        {
            MusicBoxSlot slot = collider.GetComponent<MusicBoxSlot>();
            if(slot != null)
            {
                slot.TryPlacePin();
                break;
            }
        }

        if(Slot != null) transform.position = Slot.transform.position;
        else transform.localPosition = Vector3.zero;
    }

    private void SelectThis()
    {
        if(MusicBox.Instance.SelectedPin != null)
        {
            MusicBox.Instance.SelectedPin.Deselect();
        }
        MusicBox.Instance.SelectedPin = this;
        PinSR.color = Color.white * 0.7f;
    }

    public void Deselect()
    {
        PinSR.color = Color.white;
    }
}
