using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private DirectionID currentDirection;
    [SerializeField] private RoomID currentRoom;

    void Update()
    {
        int rotation = 0;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) rotation += 1;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) rotation -= 1;

        if (Input.GetKeyDown(KeyCode.Space)) SwitchRoom(currentRoom);

        if (rotation != 0) RotatePlayer(rotation);
    }

    void RotatePlayer(int delta)
    {
        int wallCount = RoomManager.Instance.GetRoomWallList(currentRoom).Count;
        RoomManager.Instance.SetRoomActive(currentRoom, (int) currentDirection, false);
        currentDirection += delta;

        if ((int) currentDirection < 0) currentDirection = (DirectionID) wallCount - math.abs(delta);
        if ((int) currentDirection >= wallCount) currentDirection = (DirectionID) delta - 1;

        RoomManager.Instance.SetRoomActive(currentRoom, (int) currentDirection, true);
    }

    void SwitchRoom(RoomID room)
    {
        RoomManager.Instance.SetRoomActive(currentRoom, (int) currentDirection, false);

        switch(room)
        {
            case RoomID.BODY:
                currentRoom = RoomID.SOUL;
                break;
            case RoomID.SOUL:
                currentRoom = RoomID.BODY;
                break;
            default:
                Debug.Log("Could not switch rooms, invalid room input");
                break;
        }

        RoomManager.Instance.SetRoomActive(currentRoom, (int) currentDirection, true);
    }
}
