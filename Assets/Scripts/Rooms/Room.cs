using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomID ID;
    public List<Wall> Walls;

    public Wall GetWallByID(DirectionID direction)
    {
        foreach (Wall w in Walls)
        {
            if(w.Direction == direction) return w;
        }

        Debug.Log($"Wall could not be found, {direction} not a valid direction ID");
        return null;
    }
}
