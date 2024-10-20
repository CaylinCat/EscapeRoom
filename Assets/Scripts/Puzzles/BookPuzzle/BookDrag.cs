using UnityEngine;

public class BookDrag : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private SlotManager slotManager;

    void Start()
    {
        mainCamera = Camera.main;
        slotManager = FindObjectOfType<SlotManager>();
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPosition() + offset;
        transform.position = newPosition;
    }

    void OnMouseUp()
    {
        int closestSlotIndex = slotManager.GetClosestSlotIndex(transform.position);
        if (closestSlotIndex != -1)
        {
            slotManager.MoveBook(gameObject, closestSlotIndex);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mainCamera.nearClipPlane;
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}