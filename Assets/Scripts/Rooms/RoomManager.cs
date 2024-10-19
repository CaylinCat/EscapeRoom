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
    public Sprite SpiritInventorySprite;
    private bool bodyRoomActive = true;

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
    }

    void Update()
    {
        if(Input.GetButtonDown("Switch Rooms")) SwitchRooms();
    }

    private void SwitchRooms()
    {
        PuzzleManager.Instance.HidePuzzle();

        if(bodyRoomActive)
        {
            BodyRoom.gameObject.SetActive(false);
            SpiritRoom.gameObject.SetActive(true);
            bodyRoomActive = false;
        }
        else
        {
            BodyRoom.gameObject.SetActive(true);
            SpiritRoom.gameObject.SetActive(false);
            bodyRoomActive = true;
        }

        UpdateInventoryBarSprite();
    }

    private void UpdateInventoryBarSprite()
    {
        if (bodyRoomActive)
        {
            InventoryBar.sprite = BodyInventorySprite;
        }
        else
        {
            InventoryBar.sprite = SpiritInventorySprite;
        }
    }
}
