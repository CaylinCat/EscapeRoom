using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class InnerCircle : MonoBehaviour
{
    public delegate void InnerCircleCompleteHandler();
    public event InnerCircleCompleteHandler OnInnerCircleComplete;
    public List<GameObject> IngredientSlots;
    private List<ItemID> CorrectInnerPlacement;
    private List<ItemID> CurrentInnerPlacement;
    void Start() {
        CorrectInnerPlacement = new List<ItemID> {ItemID.FINGER_BONE, ItemID.HAIR, ItemID.PHOTOGRAPH, ItemID.MIRROR, ItemID.RING, ItemID.DRAGON_TOOTH, ItemID.HEART, ItemID.AMULET};
        CurrentInnerPlacement = new List<ItemID> {ItemID.NONE, ItemID.NONE, ItemID.NONE, ItemID.NONE, ItemID.NONE, ItemID.NONE, ItemID.NONE, ItemID.NONE};
    }

    public void AddItemToSlot(NRIngredientSlot slot, ItemID itemID)
    {
            CurrentInnerPlacement[slot.Index] = itemID;
            if (AreInnerItemsCorrect()) 
            {
                Debug.Log("Inner Circle Complete!!");
                OnInnerCircleComplete?.Invoke();
            }
    }

    private bool AreInnerItemsCorrect() 
    {
        return CorrectInnerPlacement.All(item => CurrentInnerPlacement.Contains(item)); // dont check order here, just if its in the circle
    }   
}