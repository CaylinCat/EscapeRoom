using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls which room is currently active and handles switching between the two rooms.
/// </summary>
public enum RoomID
{
    BODY,
    SOUL
}

public enum DirectionID
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    public Room BodyRoom;
    public Room SpiritRoom;
    private bool bodyRoomActive = true;

    public List<Room> Rooms;

    [Header("Starting Room")]
    public RoomID StartRoom;
    public DirectionID StartDirection;

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
    }

    void Update()
    {
        if(Input.GetButtonDown("Switch Rooms")) SwitchRooms();
    }

    private void SwitchRooms()
    {
        // ETHAN VERSION

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

        // NATHAN VERSION

        foreach(Room r in Rooms)
        {
            foreach(Wall w in r.Walls) w.SetWallActive(false);
        }
        GetRoomByID(StartRoom).GetWallByID(StartDirection).SetWallActive(true);
    }

    public Room GetRoomByID(RoomID room)
    {
        foreach(Room r in Rooms)
        {
            if(r.ID == room) return r;
        }

        Debug.Log($"Room could not be found, {room} not a valid room ID");
        return null;
    }
}
