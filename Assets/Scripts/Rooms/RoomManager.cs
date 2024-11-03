using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls which room is currently active and handles switching between the two rooms.
/// </summary>
public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    public Room BodyRoom;
    public Room SpiritRoom;
    public Image InventoryBar;
    public Sprite BodyInventorySprite;
    public Sprite SoulInventorySprite;
    private bool bodyRoomActive = true;
    private Vector2 cursorOffset = new Vector2(5, 0);
    [SerializeField] private Texture2D cursorStudy, cursorArmory;
    public GameObject timeDisplay;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the RoomManager singleton!");
            Destroy(this);
        }
    }

    void Start()
    {
        BodyRoom.gameObject.SetActive(true);
        SpiritRoom.gameObject.SetActive(false);
        UpdateInventoryBarSprite();
        Cursor.SetCursor(cursorStudy, cursorOffset, CursorMode.Auto);
        Cursor.visible = true;
    }

    void Update()
    {
        if(Input.GetButtonDown("Switch Rooms")) SwitchRooms();
    }

    private void SwitchRooms()
    {
        PuzzleManager.Instance.HidePuzzle();
        TransitionManager.Instance.SwitchRoomsTransition(SetRoom);
        
    }

    private void SetRoom()
    {
        if(bodyRoomActive)
        {
            BodyRoom.gameObject.SetActive(false);
            SpiritRoom.gameObject.SetActive(true);
            bodyRoomActive = false;
            Cursor.SetCursor(cursorArmory, cursorOffset, CursorMode.Auto);
        }
        else
        {
            BodyRoom.gameObject.SetActive(true);
            SpiritRoom.gameObject.SetActive(false);
            bodyRoomActive = true;
            Cursor.SetCursor(cursorStudy, cursorOffset, CursorMode.Auto);
        }
        UpdateInventoryBarSprite();
        if (timeDisplay != null)
        {
            timeDisplay.SetActive(false);
        }
    }

    private void UpdateInventoryBarSprite()
    {
        if (bodyRoomActive)
        {
            InventoryBar.sprite = BodyInventorySprite;
        }
        else
        {
            InventoryBar.sprite = SoulInventorySprite;
        }
    }
}
