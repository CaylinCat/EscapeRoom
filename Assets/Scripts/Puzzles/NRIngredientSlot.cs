using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NRIngredientSlot : MonoBehaviour
{
public bool IsOccupied = false; // true if inner circle ingredient slot has item in it
public Item PlacedItem; // the item to be entered into the ingredient slot
private Image slotImage; // Reference to the Image component


    public void DoesThisWork()
    {
        Debug.Log("This works!");
    }
}