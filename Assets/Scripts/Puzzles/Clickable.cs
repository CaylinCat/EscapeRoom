using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public int entityIndex;
    public string actionType;
    public Bricks bricksManager;

    void OnMouseDown()
    {
        switch (actionType)
        {
            case "Brick":
                if (bricksManager != null)
                {
                    bricksManager.OnEntityClicked(GetComponent<Clickable>().entityIndex);
                }
                break;

            case "OtherAction":
                // Handle other actions for different clickable objects
                Debug.Log("Other action triggered for: " + gameObject.name);
                break;
        }
    }
}