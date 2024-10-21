using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OuterCircle : MonoBehaviour
{
    public delegate void OuterCircleCompleteHandler();
    public event OuterCircleCompleteHandler OnOuterCircleComplete;
    public List<GameObject> OuterRunes; // Clickable circles in the outer ring
    public List<Sprite> SymbolSprites; // Array of the symbol sprites in the options order
    // public List<Sprite> AcceptedItemSprites; // Array of the accepted items

    private List<int> CorrectOuterPlacement; // The order in which entities should be arranged in the outer ring (clockwise)
    private List<int> OuterOptionsOrder; // The order in which available entities to place in the outer ring are cycled through

    private List<(int, int)> CurrentOuterPlacement; // The order in which the entities are currently arranged in the outer ring and thier corresponding cycle index (clockwise)

    private bool completed = false;
    void Start()
    {
        // Randomized order options cycle
        OuterOptionsOrder = new List<int> { 0, 7, 3, 5, 1, 8, 4, 2, 6 }; 
        // Correct outer order (clockwise)
        CorrectOuterPlacement = new List<int> { 8, 4, 6, 1, 5, 2, 7, 3}; 
        // Outer ring remains empty at start
        CurrentOuterPlacement = new List<(int value, int cycleIndex)> {(0,0), (0,0), (0,0), (0,0), (0,0), (0,0), (0,0), (0,0)}; 
    }

    public void InteractRune(CircleRune rune)
    {
        if (completed) return;
        
        int index = rune.Index;

        int cycleIndex = CurrentOuterPlacement[index].Item2;
        int newCycleIndex = (cycleIndex + 1) % OuterOptionsOrder.Count; 

        // Update the outer ring with its new value and index in the entity cycle
        int newValue = OuterOptionsOrder[newCycleIndex];
        CurrentOuterPlacement[index] = (newValue, newCycleIndex);   

        UpdateCircleSprite(OuterRunes[index], newCycleIndex);

        // If all entities are not set to their default value, check if the order is correct
        if (CurrentOuterPlacement.TrueForAll(item => item != (0, 0))) 
        {
            if (IsOuterOrderCorrect()) 
            {
                completed = true;
                Debug.Log("Outer Circle Complete!!");
                OnOuterCircleComplete?.Invoke();
            }
        }    }

    private void UpdateCircleSprite(GameObject circle, int spriteIndex)
    {
        SpriteRenderer spriteRenderer = circle.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Get the corresponding sprite from the SymbolSprites list
            if (spriteIndex >= 0 && spriteIndex < SymbolSprites.Count)
            {
                spriteRenderer.sprite = SymbolSprites[spriteIndex];
            }
            else
            {
                Debug.LogWarning("Sprite index is out of bounds.");
            }
        }
    }

    private bool IsOuterOrderCorrect() 
    {
        for (int i = 0; i < CorrectOuterPlacement.Count; i++)
        {
            if (CurrentOuterPlacement[i].Item1 != CorrectOuterPlacement[i])
            {
                return false;
            }
        }
        return true;
    }
}

