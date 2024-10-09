using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomID
{
    BODY,
    SOUL
}

public enum DirectionID
{
    NORTH,
    EAST,
    WEST,
    SOUTH
}

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    public List<GameObject> BodyRoomWalls;
    public List<GameObject> SoulRoomWalls;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else
        {
            Debug.LogWarning("Tried to create more than one instance of the RoomManager singleton!");
            Destroy(this);
        }

        foreach (GameObject go in BodyRoomWalls) go.SetActive(false);
        foreach (GameObject go in SoulRoomWalls) go.SetActive(false);
        SetRoomActive(RoomID.BODY, (int) DirectionID.NORTH, true);
    }

    public void SetRoomActive(RoomID room, int index, bool state)
    {
        GetRoomWallList(room)[index].SetActive(state);
    }

    public List<GameObject> GetRoomWallList(RoomID room)
    {
        switch (room)
        {
            case RoomID.BODY:
                return BodyRoomWalls;
            case RoomID.SOUL:
                return SoulRoomWalls;
            default:
                return null;
        }
    }
}
