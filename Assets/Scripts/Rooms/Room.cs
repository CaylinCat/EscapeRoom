using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles switching between an arbitrary number of walls.
/// The wall at index 0 will appear first.
/// </summary>
public class Room : MonoBehaviour
{
    // ETHAN VERSION

    public List<GameObject> Walls;
    private int wallIndex = 0;

    void Start()
    {
        foreach(GameObject wall in Walls)
        {
            wall.SetActive(false);
        }

        wallIndex = 0;
        Walls[wallIndex].SetActive(true);
    }

    void Update()
    {
        if(Input.GetButtonDown("Right"))
        {
            SwitchWall(true);
        }
        else if(Input.GetButtonDown("Left"))
        {
            SwitchWall(false);
        }
    }

    private void SwitchWall(bool direction)
    {
        Walls[wallIndex].SetActive(false);

        if(direction) 
        {
            wallIndex++;
            if(wallIndex >= Walls.Count) wallIndex = 0;
        }
        else 
        {
            wallIndex--;
            if(wallIndex < 0) wallIndex = Walls.Count - 1;
        }

        Walls[wallIndex].SetActive(true);
    }

    // NATHAN VERSION

    public RoomID ID;
    public List<Wall> WallList;

    public Wall GetWallByID(DirectionID direction)
    {
        foreach (Wall w in WallList)
        {
            if(w.Direction == direction) return w;
        }

        Debug.Log($"Wall could not be found, {direction} not a valid direction ID");
        return null;
    }
}
