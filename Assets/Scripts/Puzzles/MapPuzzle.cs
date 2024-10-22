using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPuzzle : MonoBehaviour
{
    public static bool ObtainedMapKey;
    public GameObject MapKey;
    private bool keyAdded;

    void Start() { keyAdded = false;}

    void OnEnable()
    {
        if(!keyAdded && ObtainedMapKey)
        {
            keyAdded = true;
            InventoryManager.Instance.RemoveItemByID(ItemID.MAP_KEY);
            MapKey.SetActive(true);
        }
    }
}
