using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls Instance;

    [SerializeField] private DirectionID _currentDirection;
    [SerializeField] private Room _currentRoom;
    [SerializeField] private Wall _currentWall;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the PlayerControls singleton!");
            Destroy(this);
        }

        _currentDirection = RoomManager.Instance.StartDirection;
        _currentRoom = RoomManager.Instance.GetRoomByID(RoomManager.Instance.StartRoom);
        _currentWall = _currentRoom.GetWallByID(_currentDirection);
    }

    void Update()
    {
        int rotation = 0;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) rotation += 1;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) rotation -= 1;

        if (Input.GetKeyDown(KeyCode.Space)) SwitchRoom(_currentRoom);

        if (rotation != 0) RotatePlayer(rotation);
    }

    void RotatePlayer(int delta)
    {
        int wallCount = _currentRoom.Walls.Count;
        _currentWall.SetWallActive(false);
        _currentDirection += delta;

        if ((int) _currentDirection < 0) _currentDirection = (DirectionID) wallCount - math.abs(delta);
        if ((int) _currentDirection >= wallCount) _currentDirection = (DirectionID) delta - 1;

        _currentWall = _currentRoom.GetWallByID(_currentDirection);
        _currentWall.SetWallActive(true);
    }

    void SwitchRoom(Room room)
    {
        _currentWall.SetWallActive(false);

        switch(room.ID)
        {
            case RoomID.BODY:
                _currentRoom = RoomManager.Instance.GetRoomByID(RoomID.SOUL);
                break;
            case RoomID.SOUL:
                _currentRoom = RoomManager.Instance.GetRoomByID(RoomID.BODY);
                break;
            default:
                Debug.Log("Could not switch rooms, invalid room input");
                break;
        }

        _currentWall = _currentRoom.GetWallByID(_currentDirection);
        _currentWall.SetWallActive(true);
    }
}
