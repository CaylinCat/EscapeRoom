using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public List<GameObject> entities; // Assign your clickable entities in the Inspector
    private List<int> correctOrder; // The order in which entities should be clicked
    private List<int> clickedOrder; // The order in which entities were clicked

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the correct order (this should match the indices of entities)
        correctOrder = new List<int> { 0, 1, 2, 3, 4, 5, 6 }; // Example correct order
        clickedOrder = new List<int>();
    }

    // Call this method when an entity is clicked
    public void OnEntityClicked(int index)
    {
        clickedOrder.Add(index);

        if (clickedOrder.Count > correctOrder.Count)
        {
            // Reset if clicked more than necessary
            clickedOrder.Clear();
        }
        else if (clickedOrder.Count == correctOrder.Count)
        {
            // Check if the clicked order matches the correct order
            if (IsOrderCorrect())
            {
                TriggerEvent();
            }
            else
            {
                // Reset if the order is incorrect
                clickedOrder.Clear();
            }
        }
    }
    
     private bool IsOrderCorrect()
    {
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (clickedOrder[i] != correctOrder[i])
            {
                return false;
            }
        }
        return true;
    }

    private void TriggerEvent()
    {
        Debug.Log("Correct order clicked! Event triggered!");
        // TODO: Add event upon trigger
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
