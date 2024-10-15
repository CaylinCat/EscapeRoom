using System.Collections.Generic;
using UnityEngine;

public class NecromancerCircle : MonoBehaviour
{
    public List<GameObject> outerRing; // Assign your clickable entities in the Inspector
    public List<Sprite> symbolSprites; // Array of the symbol sprites in the options order
    private List<int> correctOuterPlacement; // The order in which entities should be arranged in the outer ring (clockwise)
    private List<int> outerOptionsOrder; // The order in which available entities to place in the outer ring are cycled through

    private List<(int, int)> currentOuterPlacement; // The order in which the entities are currently arranged in the outer ring and thier corresponding cycle index (clockwise)

    void Start()
    {
        // Randomized order options cycle
        outerOptionsOrder = new List<int> { 0, 7, 3, 5, 1, 8, 4, 2, 6 }; 
        // Correct outer order (clockwise)
        correctOuterPlacement = new List<int> { 8, 4, 6, 1, 5, 2, 7, 3}; 
        // Outer ring remains empty at start
        currentOuterPlacement = new List<(int value, int cycleIndex)> {(0,0), (0,0), (0,0), (0,0), (0,0), (0,0), (0,0), (0,0)}; 
    }

    // Call this method when an entity is clicked
    public void OnEntityClicked(int index)
    {
        // Increment the cycleIndex by 1, loop back through array if at end
        int cycleIndex = currentOuterPlacement[index].Item2;
        int newCycleIndex = (cycleIndex + 1) % outerOptionsOrder.Count; 

        // Update the outer ring with its new value and index in the entity cycle
        int newValue = outerOptionsOrder[newCycleIndex];
        currentOuterPlacement[index] = (newValue, newCycleIndex);   

        UpdateCircleSprite(outerRing[index], newCycleIndex);

        // If all entities are not set to their default value, check if the order is correct
        if (currentOuterPlacement.TrueForAll(item => item != (0, 0))) 
        {
            if (IsOrderCorrect()) 
            {
                TriggerEvent();
            }
        }

    }

    private void UpdateCircleSprite(GameObject circle, int spriteIndex)
    {
        SpriteRenderer spriteRenderer = circle.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Get the corresponding sprite from the symbolSprites list
            if (spriteIndex >= 0 && spriteIndex < symbolSprites.Count)
            {
                spriteRenderer.sprite = symbolSprites[spriteIndex];
            }
            else
            {
                Debug.LogWarning("Sprite index is out of bounds.");
            }
        }
    }

    private bool IsOrderCorrect() 
    {
        for (int i = 0; i < correctOuterPlacement.Count; i++)
        {
            if (currentOuterPlacement[i].Item1 != correctOuterPlacement[i])
            {
                return false;
            }
        }
        return true;
    }

    private void TriggerEvent()
    {
        Debug.Log("Correct arrangement! Event triggered!");
        // TODO: Add event upon trigger
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}